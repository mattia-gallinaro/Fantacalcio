using System;
using System.IO;
using System.Collections.Generic;

namespace Fantacalcio
{
    class Calciatore
    {
        public string nome_e_cognome { get; set; }
        string ruolo;
        string sqaudra;
        public int costo { get; set; }
        int PunteggioPartita;
        bool gia_assegnato;
        //public Calciatore(string nome_e_cognome, string ruolo, string sqaudra)
        //{
        //    this.nome_e_cognome = nome_e_cognome;
        //    this.ruolo = ruolo;
        //    this.sqaudra = sqaudra;
        //}
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
        public string nome { get; set; }
        int punteggio;
        int crediti;
        List<Calciatore> Rosa = new List<Calciatore>();
        List<Calciatore> Formazione = new List<Calciatore>();
        public bool ControlloNomeInserito(string risposta, string[] nomigiocatori)
        {
            bool verifica = false;
            if(risposta == "")
            {
                verifica = true;
                return verifica;
            }
            if (nomigiocatori.Length > 0)
            {
                for (int i = 0; i < nomigiocatori.Length; i++)
                {
                    if(risposta == nomigiocatori[i])
                    {
                        verifica = true;
                    }
                }
                return verifica;
            }
            else
            {
                return verifica;
            }
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
            string[] nomigiocatori = new string[0];
            string filecalciatoripath = AppDomain.CurrentDomain.BaseDirectory + "Calciatori.txt";
            calciatorifile = File.ReadAllLines(filecalciatoripath);
            List<Calciatore> calciatori = new List<Calciatore>();
            Calciatore c = new Calciatore();
            Giocatori g = new Giocatori();
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
            do
            {
                Console.WriteLine("Quanti giocatori vuoi inserire ? Il numero minimo e' 2 ed il massimo e' 10");
                risposta = Console.ReadLine();
            } while (!ControlloQuantità(risposta));

            int quantità = int.Parse(risposta);

            for(int i = 0; i < quantità;i++)
            {
                Console.WriteLine("inserisci il nome del "+ (i+1) + "° giocatore");
                risposta = Console.ReadLine();
                if (g.ControlloNomeInserito(risposta, nomigiocatori))
                {
                    i--;
                }
                else
                {
                    Array.Resize(ref nomigiocatori, nomigiocatori.Length + 1);
                    nomigiocatori[i] = risposta;
                }
            }
            List<Giocatori> giocatori = new List<Giocatori>();
            for(int i = 0; i < nomigiocatori.Length; i++)
            {
                giocatori.Add(new Giocatori());
                giocatori[i].nome = nomigiocatori[i];
            }
            //chiedo all'utente il calciatore da mettere all'asta
            //chiedo all'utente le formazioni di ogni giocatore
            Console.WriteLine("Hello World!");
        }
        private static bool ControlloQuantità(string risposta)
        {
            try
            {
                int numero = int.Parse(risposta);
                if(numero < 2 || numero > 10)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
