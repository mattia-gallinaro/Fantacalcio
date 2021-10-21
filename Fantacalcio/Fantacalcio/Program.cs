using System;
using System.IO;
using System.Collections.Generic;

namespace Fantacalcio
{
    class Calciatore
    {
        string nome_e_cognome;
        string ruolo;
        string sqaudra;
        int costo;
        int PunteggioPartita;
        public List<Calciatore> GetCalciatori(string[] array)
        {
            List<Calciatore> calciatorifile = new List<Calciatore>();

            return calciatorifile;
        }

    }
    class Giocatori
    {
        string nome;
        int punteggio;
        int crediti;
        List<Calciatore> Rosa = new List<Calciatore>();
        List<Calciatore> Formazione = new List<Calciatore>();
        public void ControlloNomeInserito()
        {

        }
        public void Asta()
        {

        }
        public void AssegnazioneRosa()
        {

        }
        public void AssegnazioneFormazione()
        {

        }
    }
    
    class Partita
    {
        public void GenerazionePartita()
        {

        }
        public void AssegnazionePunteggioCalciatore()
        {

        }
        public void DichiarazioneVincitore()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] calciatorifile;
            string filecalciatoripath = AppDomain.CurrentDomain.BaseDirectory + "Calciatori.txt";
            calciatorifile = File.ReadAllLines(filecalciatoripath);
            List<Calciatore> calciatori = new List<Calciatore>();
            Calciatore c = new Calciatore();
            calciatori = c.GetCalciatori(calciatorifile);
            Console.WriteLine("Hello World!");
        }
    }
}
