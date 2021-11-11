using System;
using System.IO;
using System.Collections.Generic;

namespace Fantacalcio
{
    class Calciatore
    {
        public string nome_e_cognome { get; set; }
        public string ruolo { get; set; }
        public string sqaudra { get; set; }
        public int costo { get; set; }
        public int PunteggioPartita { get; set; }
        public bool gia_assegnato { get; set; }
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
        public int ControlloCalciatoreAsta(ref List<Calciatore> calciatori, string risposta)
        {
            bool controllo = false;
            for(int i = 0; i < calciatori.Count && !controllo; i++)
            {
                if(calciatori[i].nome_e_cognome == risposta)
                {
                    if (calciatori[i].gia_assegnato)
                    {
                        return -1;
                    }
                    else
                    {
                        controllo = true;
                        return i;
                    }
                }
            }
            return -1;
        }
            
    }
    class Giocatori
    {
        public string nome { get; set; }
        public int punteggio { get; set; }
        public int crediti { get; set; }
        public List<Calciatore> Rosa = new List<Calciatore>();
        public List<Calciatore> Formazione = new List<Calciatore>();
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
                if(valore < 0 || valore > crediti)
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
                    //Console.WriteLine($"\n{prezzi[i]}");
                }        
            }
            int m = 0;
            for(int j = 0; j < prezzi.Length; j++)
            {
                if(prezzi[j] == max)
                {
                    Array.Resize(ref indici, indici.Length + 1);
                    indici[m] = j;
                    //Console.WriteLine($"\n{j}\n{indici[m]}");
                    m++;
                }
            }
            if(indici.Length == 1)
            {
                //Console.WriteLine($"\n{indici[0]}");
                return indici[0];
            }
            else
            {
                Random random = new Random();
                int numero_generato = random.Next(0, indici.Length);
                //Console.WriteLine($"\n{indici[numero_generato]}"); era per controllare la generazione dell'indice del giocatore che ottiene il calciatore durante l'asta
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
            Rosa.Add(new Calciatore());
            Rosa[Rosa.Count-1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;
            Rosa[Rosa.Count-1].sqaudra = calciatores[indice_c].sqaudra;
            Rosa[Rosa.Count-1].ruolo = calciatores[indice_c].ruolo;
            Rosa[Rosa.Count-1].costo = calciatores[indice_c].costo;
            crediti -= Rosa[Rosa.Count-1].costo;

        }
        public int ControlloGiocatoreFormazione(ref List<Calciatore> calciatores, string risposta)
        {
            int n = -1; 
            for(int i = 0; i < calciatores.Count; i++)
            {
                if(calciatores[i].nome_e_cognome == risposta)
                {
                    if (!calciatores[i].gia_assegnato)
                    {
                        n = i;
                    }
                }        
            }
            return n;
        }
            
        public void AssegnazioneFormazione(ref List<Calciatore> calciatores, int indice_c)
        {
            Formazione.Add(new Calciatore());
            Formazione[Rosa.Count - 1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;
            Formazione[Rosa.Count - 1].sqaudra = calciatores[indice_c].sqaudra;
            Formazione[Rosa.Count - 1].ruolo = calciatores[indice_c].ruolo;
            Formazione[Rosa.Count - 1].costo = calciatores[indice_c].costo;
            crediti -= Rosa[Rosa.Count - 1].costo;
        }
    }
    
    class Partita
    {
        public Partita()
        {

        }
        public int[] GenerazionePartita(int quantita)
        {
            int[] array = new int[quantita];//array che contiene gli indici da generare per le partite
            Random random = new Random();//crea un'istanza della classe Random
            int numero_generato = 0;//variabile che contiene il numero generato
            bool controllo = false;//variabile booleana usata per il controllo nella generazione di doppioni
            for (int i = 0; i < array.Length; i++)//for che si ripete per il numero di squadre inserite dall'utente
            {
                numero_generato = random.Next(0, quantita);//viene richiamata la funzione Next della classe Random dando come parametri 0 ed quantità
                if (i >= 1)//inizia a controllare appena avrà generato il secondo numero
                {
                    for (int j = 0; j < i; j++)//for usato per il controllo dei doppioni
                                               //si ripete fino a quando j raggiunge il valore di i
                    {
                        if (array[j] == numero_generato)//controlla che il numero appena generato non corrisponda ad un numero già presente in array
                        {
                            controllo = true;//se è un doppione allora cambio il valore di controllo e lo rendo true
                        }

                    }
                }
                else//se è il primo numero ad esse generato
                {
                    array[i] = numero_generato;
                }
                if (controllo)//se si tratta di un doppione fa ripetere la generazione del numero 
                {
                    i--;//diminuisce il valore di i di 1 per far ripetere la generazione del numero
                    controllo = false;//riporto il valore di controllo a false sennò non terminerebbe mai la generazione dei numeri
                }
                else//se il numero generato non è doppione
                {
                    array[i] = numero_generato;//salvo il numero appena generato in array nella posizione i
                }
            }
            return array;//ritorno l'array con gli indici delle partite
        }
        public void AssegnazionePunteggioCalciatore()
        {

        }
        public void Classifica(ref List<Giocatori> giocatori)
        {
            bool controllo;//variabile booleana
            do
            {
                controllo = false;//riassegna false a controllo sennò continuerebbe all'infinito
                for (int i = 0; i < (giocatori.Count - 1); i++)//il ciclo si ripete per la quantità di squadre presenti nella lista bubble diminuito di 1
                {
                    if (giocatori[i].punteggio < giocatori[i + 1].punteggio)//controlla se il punteggio della squadra in posizione i della lista bubble sia minore del punteggio della squadra in posizione successiva ad i nella lista bubbble
                    {
                        giocatori.Reverse(i, 2);//scambio i due elementi della lista 
                        controllo = true;
                    }
                }
            } while (controllo);//si ripete fino a quando il valore della variabile booleana corrisponde a false
        }
        public int GestionePartita()
        {
            int n = 0;

            return n;
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
            Partita p = new Partita();
            calciatori = c.GetCalciatori(calciatorifile);
            for(int i = 0; i < calciatori.Count; i++)
            {
                Console.WriteLine($"{calciatori[i].nome_e_cognome}");
            }
            string risposta;
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
                giocatori[i].crediti = 1100;
            }
            int[] prezzo = new int[giocatori.Count];
            int indice;
            int posizione = -1;
            //chiedo all'utente il calciatore da mettere all'asta
            do
            {
                risposta = "";
                Console.WriteLine("Inserisci il nome del calciatore da mettere all'asta (es mauro juan)");
                risposta = Console.ReadLine();
                while (posizione < 0)
                {
                    Console.WriteLine("Il giocatore inserito non esiste oppure e' gia' stato assegnato\n Inserisci un'altro giocatore");
                    risposta = Console.ReadLine();
                    posizione = c.ControlloCalciatoreAsta(ref calciatori, risposta);
                }
                int i;
                for(i = 0; i < giocatori.Count; i++)
                {
                    do
                    {
                        Console.WriteLine($"Giocatore {giocatori[i].nome} inserisci il prezzo per l'asta per il calciatore");
                        prezzo[i] = giocatori[i].ControlloPrezzoInserito();
                    } while (prezzo[i] > 110);
                }
                indice = g.Asta(prezzo);
                Console.WriteLine($"{giocatori[indice].nome} ha vinto {calciatori[posizione].nome_e_cognome}");
                giocatori[indice].AssegnazioneRosa(ref calciatori, posizione);
                if (!g.ControlloRose(ref giocatori))
                {
                    Console.WriteLine("Vuoi terminare l'asta?\n Se si allora scrivi yes \n in caso tu voglia continuare l'asta");
                }
                risposta = "";
            } while (risposta != "Yes" && risposta != "Y" && risposta != "yes" && risposta!= "y");
            Console.WriteLine("Ogni giocatore avra' una formazione composta da 11 giocatori\n la formazione per ciascun giocatore si basa sul modulo 4-3-3");
            //chiedo all'utente le formazioni di ogni giocatore
            int g_indice;
            for(g_indice = 0; g_indice < giocatori.Count; g_indice++)
            {
                Console.WriteLine($"Rosa di {giocatori[g_indice].nome}");
                for (int j= 0; j < giocatori[g_indice].Rosa.Count; j++)
                {
                    Console.WriteLine($"{giocatori[g_indice].Rosa[j].nome_e_cognome}");

                }
                for(int i  = 0; i< 12; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come portiere");
                        risposta = Console.ReadLine();
                        posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                        {
                            Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come portiere");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        }
                    }
                    else if(i <= 4)
                    {
                        Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come difensore");
                        risposta = Console.ReadLine();
                        posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                        {
                            Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come difensore");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        }
                    }
                    else if (i <= 7)
                    {
                        Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come centrocampista");
                        risposta = Console.ReadLine();
                        posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                        {
                            Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come centrocampista");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come attaccante");
                        risposta = Console.ReadLine();
                        posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                        {
                            Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come attaccante");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                        }
                    }
                    giocatori[i].AssegnazioneFormazione(ref giocatori[i].Rosa, posizione);
                }
                g_indice++;
            }
            do
            {
                Console.WriteLine("");
                int[] indici = p.GenerazionePartita(giocatori.Count);
                int n1 = 0;
                for(int i = 0; i < giocatori.Count; i+=2)
                {
                    n1++;
                    Console.WriteLine($"Girone {n1} di {indici.Length / 2}");
                    Console.WriteLine($"Si sfideranno {giocatori[indici[i]].nome} contro {giocatori[indici[i+1]].nome}");

                    int valore = p.GestionePartita(ref squadre, risposte, indici[i], indici[i + 1]);//invoca la funzione GestionePartita e il valore che ritorna viene assegnato ad risposte[5]
                    if (valore == 2)//nel caso in cui il valore ritornato corrisponde a 2
                    {
                        Console.WriteLine($"in questa partita ha vinto la squadra {giocatori[indici[i]].nome}");//viene scritto a schermo che la prima squadra delle due squadre della partita, ha vinto 
                    }
                    else if (valore == 1)//nel caso in cui il valore ritornato corrisponde a 1
                    {
                        Console.WriteLine($"In questa partita ha vinto la squadra {giocatori[indici[i + 1]].nome}");//viene scritto a schermo che la prima squadra delle due squadre della partita, ha vinto 
                    }
                    else//nel caso in cui il valore ritornato corrisponde a 0
                    {
                        Console.WriteLine("Questa partita è terminata in pareggio");//viene scritto a schermo che la partita è terminata con il pareggio delle due squadre
                    }
                }
            } while (risposta != "yes" || risposta != "y");
            Console.WriteLine(".");
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
