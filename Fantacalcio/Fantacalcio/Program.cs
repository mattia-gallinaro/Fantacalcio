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
            for (int i = 0; i < array.Length; i++)
            {
                string[] arraycalciatore = array[i].Split(',');
                calciatorifile.Add(new Calciatore());
                calciatorifile[i].nome_e_cognome = arraycalciatore[0];
                calciatorifile[i].ruolo = arraycalciatore[1];
                calciatorifile[i].sqaudra = arraycalciatore[2];
                calciatorifile[i].PunteggioPartita = 0;
                calciatorifile[i].costo = 0;
            }
            
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
            List<Giocatori> giocatoris = new List<Giocatori>();
            Console.WriteLine("Premi a se vuoi iniziare un nuovo campionato \n b per continuare un campionato gia' iniziato");
            string risposta = Console.ReadLine();
            switch (risposta)
            {
                case "a":
                    break;
                case "b":
                    break;
            }
            //chiedo all'utente quanti giocatori devono essere inseriti ed il loro nome
            Console.WriteLine("Premi a se vuoi iniziare un nuovo campionato \n b per continuare un campionato gia' iniziato");
            risposta = Console.ReadLine();
            //chiedo all'utente il calciatore da mettere all'asta
            //chiedo all'utente le formazioni di ogni giocatore
            Console.WriteLine("Hello World!");
        }
    }
}
