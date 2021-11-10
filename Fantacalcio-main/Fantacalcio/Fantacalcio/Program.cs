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
        public Calciatore()
        {
        }
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
                calciatorifile[i].gia_assegnato = false;
            }
            
            return calciatorifile;
        }
        public bool ControlloCalciatoreAsta(List<Calciatore> calciatori, string risposta)
        {
            bool controllo = false;
            for(int i = 0; i < calciatori.Count && !controllo; i++)
            {
                if(calciatori[i].nome_e_cognome == risposta)
                {
                    if (calciatori[i].gia_assegnato)
                    {
                        return true;
                    }
                    else
                    {
                        controllo = true;
                    }
                }
            }
            return controllo;
        }
            
    }
    class Giocatori
    {
        public string nome { get; set; }
        //int punteggio;
        public int crediti { get; set; }
        List<Calciatore> Rosa = new List<Calciatore>();
        List<Calciatore> Formazione = new List<Calciatore>();
        public bool ControlloNomeInserito(string risposta, string[] nomigiocatori)
        {
            bool verifica = false;
            if(risposta == "" || risposta.Contains(","))
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
        public int ControlloPrezzoInserito()
        {
            try
            {
                int valore = int.Parse(Console.ReadLine());
                if(valore < 0 || valore > this.crediti)
                {
                    return 111;
                }
                else
                {
                    return valore;
                }     
            }
            catch
            {
                return 111;
            }
        }
        public int Asta(int[] prezzi)
        {
            int[] indici = new int[0];
            int max = -1;
            for(int i = 0; i< prezzi.Length; i++)
            {
                if(max < prezzi[i])
                {
                    max = prezzi[i];
                    Console.WriteLine($"{prezzi[i]}");
                }        
            }
            int m = 0;
            for(int j = 0; j < prezzi.Length; j++)
            {
                if(prezzi[j] == max)
                {
                    Array.Resize(ref indici, indici.Length + 1);
                    indici[m] = j;
                    m++;
                }
            }
            if(indici.Length == 1)
            {
                return indici[0];
            }
            else
            {
                Random random = new Random();
                int numero_generato = random.Next(0, indici.Length);
                return indici[numero_generato];
            }
        }
        public bool ControlloRose(ref List<Giocatori> giocatoris)
        {
            bool controllo = false;
            for (int i = 0; i < giocatoris.Count; i++)
            {
                if (giocatoris[i].Rosa.Count < 11)
                {
                    controllo = true;
                }
            }
            return controllo;
        }
        public void AssegnazioneRosa(ref List<Calciatore> calciatores, int indice_c)
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
            int[] prezzo = new int[giocatoris.Count];
            int indice;
            //chiedo all'utente il calciatore da mettere all'asta
            do
            {
                risposta = "";
                Console.WriteLine("Inserisci il nome del calciatore da mettere all'asta (es mauro juan)");
                risposta = Console.ReadLine();
                while (c.ControlloCalciatoreAsta(calciatori, risposta))
                {
                    Console.WriteLine("Il giocatore inserito non esiste oppure e' gia' stato assegnato\n Inserisci un'altro giocatore");
                    risposta = Console.ReadLine();
                }
                int i;
                for(i = 0; i < giocatoris.Count; i++)
                {
                    do
                    {
                        Console.WriteLine($"Giocatore {giocatoris[i].nome} inserisci il prezzo per l'asta per il calciatore");
                        prezzo[i] = giocatoris[i].ControlloPrezzoInserito();
                    } while (prezzo[i] > 110);
                }
                indice = g.Asta(prezzo);
                giocatoris[i].AssegnazioneRosa(ref calciatori, indice);
                if (!g.ControlloRose(ref giocatoris))
                {
                    Console.WriteLine("Vuoi terminare l'asta?\n Se si allora scrivi yes \n in caso tu voglia continuare l'asta ");
                }
            } while (risposta != "Yes" && risposta != "Y" && risposta != "yes" && risposta!= "y");
            
            //chiedo all'utente le formazioni di ogni giocatore
            Console.WriteLine("Hello World!");
        }
        private static bool ControlloQuantità(string risposta)
        {
            try
            {
                int numero = int.Parse(risposta);
                if (numero < 2 || numero > 10)
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
