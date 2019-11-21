using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deenee {
    public class ChatDice : IChatInteractable {
        public class ChatDiceEntry : IChatEntry {
            public string Text {
                get {
                    return dice.NumberResult + " = " + dice.StringResult;
                }
            }
            private Dice dice;
            public ChatDiceEntry(Dice dice) {
                this.dice = dice;
            }
        }
        public string[] Comamnds { get; } = new string[] { "r", "roll" };

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
        private static readonly char[] operatorOrder = new char[] { '+', '-', '*', '/', '^', 'd' };
        private Node rootNode;
        public Dice(string diceString) {
            diceString = Regex.Replace(diceString, @"\s+", "");
            rootNode = new Node(diceString);
            void traverse(Node node) {
                if (node.Value[0] == '(' && node.Value[node.Value.Length - 1] == ')') {
                    node.Value = node.Value.Substring(1, node.Value.Length - 2);
                }
                for (int i = 0; i < operatorOrder.Length; i++) {
                    bool inParentheses = false;
                    for (int j = 0; j < node.Value.Length; j++) {
                        if (node.Value[j] == '(') {
                            inParentheses = true;
                        }
                        else if (node.Value[j] == ')') {
                            inParentheses = true;
                        }
                        if (inParentheses) {
                            continue;
                        }
                        if (node.Value[j] == operatorOrder[i]) {
                            node.Left = new Node(node.Value.Substring(0, j));
                            node.Right = new Node(node.Value.Substring(j + 1, node.Value.Length - (j + 1)));
                            node.Value = node.Value[j].ToString();
                            traverse(node.Left);
                            traverse(node.Right);
                            return;
                        }
                    }
                }
            }
            traverse(rootNode);
        }
        public double NumberResult { get; private set; }
        public string StringResult { get; private set; }

        public void Calculate() {
            StringResult = "";
            double traverse(Node node) {
                if (node.Left == null && node.Right == null) {
                    StringResult += node.Value;
                    return float.Parse(node.Value);
                }
                else {
                    double res = 0.0;
                    StringResult += "(";
                    double left = traverse(node.Left);
                    StringResult += node.Value;
                    double right = traverse(node.Right);
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
                                int rnd = Main.Random.Next(1, (int)right + 1);
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
            NumberResult = traverse(rootNode);
        }
    }
}