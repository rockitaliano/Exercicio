using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria {
        public int Numero { get; private set; }
        public string Titular { get; private set; }
        private double _saldo;

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
            _saldo = 0.0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial) : this(numero, titular)
        {
            Deposito(depositoInicial);
        }

        public void AlterarTitular(string novoTitular)
        {
            Titular = novoTitular;
        }

        public void Deposito(double quantia)
        {
            if (quantia <= 0)
            {
                Console.WriteLine("O valor do depósito deve ser maior que zero.");
            }
            else
            {
                _saldo += quantia;
            }
        }

        public void Saque(double quantia)
        {
            if (quantia <= 0)
            {
                Console.WriteLine("O valor do saque deve ser maior que zero.");
            }            
            else
            {
                _saldo -= quantia + 3.50;
            }
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: ${_saldo.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
