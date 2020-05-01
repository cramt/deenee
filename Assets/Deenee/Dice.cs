using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deenee {
    public class ChatDice : IChatInteractable {
        public class ChatDiceEntry : IChatEntry {
            public string Text => dice.NumberResult + " = " + dice.StringResult;

            private readonly Dice dice;

            public ChatDiceEntry(Dice dice) {
                this.dice = dice;
            }
        }

        public string[] Commands { get; } = new string[] {"r", "roll"};

        public uint MinArg { get; } = 1;

        public uint MaxArg { get; } = 1;

        public IChatEntry Call(string[] args) {
            Dice dice = new Dice(args[0]);
            dice.Calculate();
            return new ChatDiceEntry(dice);
        }
    }

    [Serializable]
    public class Dice {
        [Serializable]
        public class Node {
            public Node Left { get; set; } = null;
            public Node Right { get; set; } = null;
            public string Value { get; set; } = null;

            public Node(string value) {
                Value = value;
            }
        }

        private static readonly char[] OperatorOrder = new char[] {'+', '-', '*', '/', '^', 'd'};
        private Node rootNode;

        public Dice(string diceString) {
            diceString = Regex.Replace(diceString, @"\s+", "");
            rootNode = new Node(diceString);

            void Traverse(Node node) {
                if (node.Value[0] == '(' && node.Value[node.Value.Length - 1] == ')') {
                    node.Value = node.Value.Substring(1, node.Value.Length - 2);
                }

                foreach (char t in OperatorOrder) {
                    bool inParentheses = false;
                    for (int j = 0; j < node.Value.Length; j++) {
                        switch (node.Value[j]) {
                            case '(':
                            case ')':
                                inParentheses = true;
                                break;
                        }

                        if (inParentheses) {
                            continue;
                        }

                        if (node.Value[j] != t) continue;
                        
                        node.Left = new Node(node.Value.Substring(0, j));
                        node.Right = new Node(node.Value.Substring(j + 1, node.Value.Length - (j + 1)));
                        node.Value = node.Value[j].ToString();
                        Traverse(node.Left);
                        Traverse(node.Right);
                        return;
                    }
                }
            }

            Traverse(rootNode);
        }

        public double NumberResult { get; private set; }
        public string StringResult { get; private set; }

        public void Calculate() {
            StringResult = "";

            double Traverse(Node node) {
                if (node.Left == null && node.Right == null) {
                    StringResult += node.Value;
                    return float.Parse(node.Value);
                }
                else {
                    double res = 0.0;
                    StringResult += "(";
                    double left = Traverse(node.Left);
                    StringResult += node.Value;
                    double right = Traverse(node.Right);
                    switch (node.Value[0]) {
                        case '+':
                            res = left + right;
                            break;
                        case '-':
                            res = left - right;
                            break;
                        case '*':
                            res = left * right;
                            break;
                        case '/':
                            res = left / right;
                            break;
                        case '^':
                            res = Math.Pow(left, right);
                            break;
                        case 'd':
                            List<int> results = new List<int>();
                            StringResult += "->{ ";
                            for (int i = 0; i < left; i++) {
                                int rnd = Main.Random.Next(1, (int) right + 1);
                                StringResult += "[" + rnd + "] ";
                                results.Add(rnd);
                            }

                            StringResult += "}";
                            res = results.Sum();
                            break;
                    }

                    StringResult += ")";
                    return res;
                }
            }

            NumberResult = Traverse(rootNode);
        }
    }
}