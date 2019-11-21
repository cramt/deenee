using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Deenee.TypeGenerator {
    public class Namespace {
        public string name;
        public List<Namespace> children = new List<Namespace>();
        public Namespace parent = null;
        public List<Type> types = new List<Type>();
    }
    public class Builder {
        public Namespace global;
        public Builder() {
            global = new Namespace();
            global.name = "global";
        }
        public void LoadAssembly(Assembly assembly) {
            List<Type> types = assembly.GetTypes().ToList();
            types.ForEach(type => {
                if (type.Namespace == null) {
                    return;
                }
                List<string> namespacePath = type.Namespace.Split('.').ToList();
                Namespace currNamespace = global;
                namespacePath.ForEach(subnamespace => {
                    Namespace n = currNamespace.children.FirstOrDefault(x => x.name == subnamespace);
                    if (n == null) {
                        n = new Namespace();
                        n.name = subnamespace;
                        n.parent = currNamespace;
                        currNamespace.children.Add(n);
                    }
                    currNamespace = n;
                });
                currNamespace.types.Add(type);
            });
        }

        public string Generate() {
            StringBuilder sb = new StringBuilder();

            void add(Namespace n) {
                if (n.name == "Mono" && n.parent == global) {
                    return;
                }
                if (n.parent == global) {
                    sb.Append("declare ");
                }
                sb.Append("namespace " + n.name + " {\n");
                n.children.ForEach(add);
                List<string> namesAdded = new List<string>();
                n.types.ForEach(t => {
                    string name = t.Name;
                    if (namesAdded.Contains(name)) {
                        return;
                    }
                    if (t.IsClass) {
                        if (t.IsGenericType) {
                            return;
                        }
                        sb.Append("class " + name + "{\n");

                        sb.Append("}");
                        namesAdded.Add(name);
                    }

                });

                sb.Append("}");
            }
            global.children.ForEach(add);
            return sb.ToString();
        }

        public void LoadAssemblies(IEnumerable<Assembly> assemblies) {
            assemblies.ToList().ForEach(x => {
                LoadAssembly(x);
            });
        }
    }
}