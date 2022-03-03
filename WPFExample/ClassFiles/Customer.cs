using System;
using System.Xml.Serialization;

namespace WPFExample
{
    public class Customer
    {
        private string name;
        private double cardNumber;
        private string cardTxt;
        private Property property;
        public string Name { get => name; set => name = value; }
        public double CardNumber { get => cardNumber; set => cardNumber = value; }
        public Property Property { get => property; set => property = value; }
        public string CardTxt { get => cardTxt; set => cardTxt = value; }

        public Customer() { }
        public Customer(string name, double cardNumber, Property property)
        {
            this.name = name;
            this.cardNumber = cardNumber;
            this.property = property;
            string cn = cardNumber.ToString();
            this.cardTxt = string.Format($"{cn.Substring(0, 4)} XXXX XXXX {cn.Substring(12,4)}");
        }

        public override string ToString()
        {
            string cn = CardNumber.ToString();
            return string.Format($"| Name: {Name} - Card Number: {cn.Substring(0, 4)} XXXX XXXX {cn.Substring(12, 4)} | Property: {property}");
        }

    }
}
