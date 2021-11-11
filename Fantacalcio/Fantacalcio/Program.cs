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
        public int punteggio_totale { get; set; }
        public int crediti { get; set; }
        public List<Calciatore> Rosa = new List<Calciatore>();
        public List<Calciatore> Formazione = new List<Calciatore>();
        public Giocatori()
        {

        }
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
        public void AssegnazioneRosa(ref List<Calciatore> calciatores, int indice_c,int prezzo)
        {
            Rosa.Add(new Calciatore());
            Rosa[Rosa.Count-1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;
            Rosa[Rosa.Count-1].sqaudra = calciatores[indice_c].sqaudra;
            Rosa[Rosa.Count-1].ruolo = calciatores[indice_c].ruolo;
            Rosa[Rosa.Count-1].costo = prezzo;
            calciatores[indice_c].gia_assegnato = true;
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
            Formazione[Formazione.Count - 1].nome_e_cognome = calciatores[indice_c].nome_e_cognome;
            Formazione[Formazione.Count - 1].sqaudra = calciatores[indice_c].sqaudra;
            Formazione[Formazione.Count - 1].ruolo = calciatores[indice_c].ruolo;
            Formazione[Formazione.Count - 1].costo = calciatores[indice_c].costo;
            calciatores[indice_c].gia_assegnato = true;
        }
        public void CalcolaPunteggio()
        {
            for(int i = 0; i < Formazione.Count; i++)
            {
                punteggio += Formazione[i].PunteggioPartita;
            }
            punteggio_totale += punteggio;
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
        public int AssegnazionePunteggioCalciatore()
        {
            Random random = new Random();
            int numero_generato = random.Next(2, 12);
            return numero_generato;
        }
        public void ClassificaProvvisoria(ref List<Giocatori> giocatori)
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
        public void ClassificaFinale(ref List<Giocatori> giocatori)
        {
            bool controllo;//variabile booleana
            do
            {
                controllo = false;//riassegna false a controllo sennò continuerebbe all'infinito
                for (int i = 0; i < (giocatori.Count - 1); i++)//il ciclo si ripete per la quantità di squadre presenti nella lista bubble diminuito di 1
                {
                    if (giocatori[i].punteggio_totale < giocatori[i + 1].punteggio_totale)//controlla se il punteggio della squadra in posizione i della lista bubble sia minore del punteggio della squadra in posizione successiva ad i nella lista bubbble
                    {
                        giocatori.Reverse(i, 2);//scambio i due elementi della lista 
                        controllo = true;
                    }
                }
            } while (controllo);//si ripete fino a quando il valore della variabile booleana corrisponde a false
        }
        public int GestionePartita(ref List<Giocatori> giocatores, int indice_1, int indice_2)
        {
            int n;
            for(int i  = 0; i< giocatores[indice_1].Formazione.Count; i++)
            {
                giocatores[indice_1].Formazione[i].PunteggioPartita = AssegnazionePunteggioCalciatore();
            }
            for (int i = 0; i < giocatores[indice_2].Formazione.Count; i++)
            {
                giocatores[indice_2].Formazione[i].PunteggioPartita = AssegnazionePunteggioCalciatore();
            }
            giocatores[indice_1].CalcolaPunteggio();
            giocatores[indice_2].CalcolaPunteggio();
            if(giocatores[indice_1].punteggio > giocatores[indice_2].punteggio)
            {
                n = 2;
            }
            else if (giocatores[indice_1].punteggio < giocatores[indice_2].punteggio)
            {
                n = 1;
            }
            else
            {
                n = 0;
            }
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
            if (!File.Exists(filecalciatoripath))
            {
                File.Create(filecalciatoripath);
                Console.WriteLine("Dato che non hai un file Calciatori.txt in cui sono contenuti i calciatori\n ne creo uno vuoto all'interno di esso inserisci i calciatori in questo modo\n ex. musso juan,portiere,atalanta,false");
            }
            else
            {
                calciatorifile = File.ReadAllLines(filecalciatoripath);
                List<Calciatore> calciatori = new List<Calciatore>();
                Calciatore c = new Calciatore();
                Giocatori g = new Giocatori();
                Partita p = new Partita();
                calciatori = c.GetCalciatori(calciatorifile);
                for (int i = 0; i < calciatori.Count; i++)
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

                for (int i = 0; i < quantità; i++)
                {
                    Console.WriteLine("inserisci il nome del " + (i + 1) + "° giocatore");
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
                for (int i = 0; i < nomigiocatori.Length; i++)
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
                    posizione = c.ControlloCalciatoreAsta(ref calciatori, risposta);
                    while (posizione < 0)
                    {
                        Console.WriteLine("Il giocatore inserito non esiste oppure e' gia' stato assegnato\n Inserisci un'altro giocatore");
                        risposta = Console.ReadLine();
                        posizione = c.ControlloCalciatoreAsta(ref calciatori, risposta);
                    }
                    int i;
                    for (i = 0; i < giocatori.Count; i++)
                    {
                        do
                        {
                            Console.WriteLine($"Giocatore {giocatori[i].nome} inserisci il prezzo per l'asta per il calciatore");
                            prezzo[i] = giocatori[i].ControlloPrezzoInserito();
                        } while (prezzo[i] > 110);
                    }
                    indice = g.Asta(prezzo);
                    Console.WriteLine($"{giocatori[indice].nome} ha comprato il calciatore {calciatori[posizione].nome_e_cognome}");
                    giocatori[indice].AssegnazioneRosa(ref calciatori, posizione, prezzo[indice]);
                    Console.WriteLine($"{giocatori[indice].crediti}");
                    if (!g.ControlloRose(ref giocatori))
                    {
                        Console.WriteLine("Vuoi terminare l'asta?\n Se si allora scrivi yes \n in caso tu voglia continuare l'asta");
                        risposta = Console.ReadLine();
                    }
                } while (risposta != "Yes" && risposta != "Y" && risposta != "yes" && risposta != "y");
                Console.WriteLine("Ogni giocatore avra' una formazione composta da 11 giocatori\n la formazione per ciascun giocatore si basa sul modulo 4-3-3");
                //chiedo all'utente le formazioni di ogni giocatore
                int g_indice;
                for (g_indice = 0; g_indice < giocatori.Count; g_indice++)
                {
                    Console.WriteLine($"Rosa di {giocatori[g_indice].nome}");
                    for (int j = 0; j < giocatori[g_indice].Rosa.Count; j++)
                    {
                        Console.WriteLine($"{giocatori[g_indice].Rosa[j].nome_e_cognome}");

                    }
                    for (int i = 0; i < 12; i++)
                    {
                        if (i == 0)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come portiere");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come portiere che appartenga alla rosa");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else if (i <= 4)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come difensore");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come difensore che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else if (i <= 7)
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come centrocampista");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come centrocampista che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Giocatore {giocatori[g_indice].nome} inserisci il nome del giocatore da inserire nella formazione come attaccante");
                            risposta = Console.ReadLine();
                            posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            while (posizione < 0)//pongo il controllo che il giocatore appartenga alla rosa del giocatore e non sia già stato assegnato
                            {
                                Console.WriteLine($"Giocatore {g_indice + 1} inserisci il nome del giocatore da inserire nella formazione come attaccante che non sia gia' stato inserito");
                                risposta = Console.ReadLine();
                                posizione = giocatori[g_indice].ControlloGiocatoreFormazione(ref giocatori[g_indice].Rosa, risposta);
                            }
                        }
                        giocatori[g_indice].AssegnazioneFormazione(ref giocatori[g_indice].Rosa, posizione);
                    }
                }
                do
                {
                    Console.WriteLine("");
                    int[] indici = p.GenerazionePartita(giocatori.Count);
                    int n1 = 0;
                    for (int j = 0; j < 7; j++)
                    {
                        Console.WriteLine($"Giornata {j + 1} di 7\n");
                        for (int i = 0; i < giocatori.Count; i += 2)
                        {
                            n1++;
                            Console.WriteLine($"Girone {n1} di {indici.Length / 2}");
                            Console.WriteLine($"Si sfideranno {giocatori[indici[i]].nome} contro {giocatori[indici[i + 1]].nome}");

                            int valore = p.GestionePartita(ref giocatori, indici[i], indici[i + 1]);//invoca la funzione GestionePartita e il valore che ritorna viene assegnato ad risposte[5]
                            if (valore == 2)//nel caso in cui il valore ritornato corrisponde a 2
                            {
                                Console.WriteLine($"in questa partita ha vinto la squadra {giocatori[indici[i]].nome}");//viene scritto a schermo che la prima squadra delle due squadre della partita, ha vinto 
                            }
                            else if (valore == 1)//nel caso in cui il valore ritornato corrisponde a 1
                            {
                                Console.WriteLine($"In questa partita ha vinto la squadra {giocatori[indici[i + 1]].nome}");//viene scritto a schermo che la seconda squadra delle due squadre della partita, ha vinto 
                            }
                            else//nel caso in cui il valore ritornato corrisponde a 0
                            {
                                Console.WriteLine("Questa partita è terminata in pareggio");//viene scritto a schermo che la partita è terminata con il pareggio delle due squadre
                            }
                            giocatori[indici[i]].punteggio = 0;
                            giocatori[indici[i+1]].punteggio = 0;
                        }
                        n1 = 0;
                    }
                    p.ClassificaProvvisoria(ref giocatori);
                    for(int d  = 0; d < giocatori.Count; d++)
                    {
                        Console.WriteLine($"In {d+1}° posizione abbiamo {giocatori[d].nome}");
                    }
                    Console.WriteLine("Scrivi yes per terminare il campionato");
                    risposta = Console.ReadLine();
                } while (risposta != "yes" && risposta != "y");
                p.ClassificaFinale(ref giocatori);
                for (int d = 0; d < giocatori.Count; d++)
                {
                    Console.WriteLine($"In {d + 1}° posizione abbiamo {giocatori[d].nome}");
                }
                Console.WriteLine("Grazie per aver giocato");
            }
        }
        private static bool ControlloQuantità(string risposta)
        {
            try
            {
                int numero = int.Parse(risposta);
                if (numero < 2 || numero > 10 || numero %2 != 0)
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
