using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Editor_de_Grafos
{
    public class Grafo : GrafoBase, iGrafo
    {
        private bool[] visitado;
        private string Custo;
        private int numeroCromático;
        Random aleatorio = new Random();

        public void limparCores() // Função pra limpar as cores do grafo depois de um metodo
        {

            for (int i = 0; i < getN(); i++) 
            {
                getVertice(i).setCor(Color.Chocolate);
                for (int j = 0; j < getN(); j++)
                {
                    if(getAresta(i,j)!=null)
                        getAresta(i, j).setCor(Color.Black);
                }
            }
        }
        public void AGM(int v)
        {
            limparCores();

            int[] antecessor = new int[getN()];// usado na hora de setar as arestas
            int[] estimativa = new int[getN()];// usado pra pegar sempre os menores caminhos
            bool[] fechados = new bool[getN()];// Tipo um "Visitados", só que tirei da ideia do Camino mínimo
            Custo = "";

            int custoAuxiliar = 0, min = 1000000;// O custo é uma var cumulativa pra sair no retorno no final com o valor 
            fechados[v] = true;
            for (int j=0; j< getN()-1;j++)
            {
                for (int i = 0; i < getN(); i++)
                {
                    if (getAresta(v, i) != null && !fechados[i])
                    {
                        if (estimativa[i] != 0 && estimativa[i] > getAresta(v, i).getPeso()) // serve para V ja visitados mas que possam ter estm. maior que a atual
                        {
                            estimativa[i] = getAresta(v, i).getPeso();
                            antecessor[i] = v;
                        }
                        else if (estimativa[i] == 0)
                        {
                            estimativa[i] = getAresta(v,i).getPeso();
                            antecessor[i] = v;
                        }
                    }
                }
                for (int i = 0; i < getN(); i++) // escolhe a aresta c/menor custo
                {
                    if (estimativa[i] < min && !fechados[i] && estimativa[i] != 0)
                    {
                        min = estimativa[i];
                        v = i;
                    }
                }
                custoAuxiliar += getAresta(antecessor[v], v).getPeso();
                getAresta(antecessor[v], v).setCor(System.Drawing.Color.YellowGreen);
                fechados[v] = true;
                min = 1000000;//reseta o min
            }
            Custo += custoAuxiliar;
        }
        public void caminhoMinimo(int c, int f)
        {
            limparCores();
            // Tres vetores do Djkstra
            int[] antecessor = new int[getN()];
            int[] estimativa = new int[getN()];
            bool[] fechados = new bool[getN()];
            Custo = "";

            int min = 1000000, inicial = c, destino = f;
            fechados[c] = true;
            while (c != f) { 
                for (int v = 0; v < getN(); v++)
                {
                    if (getAresta(c, v) != null && !fechados[v])
                    {
                        if (estimativa[v] != 0 && estimativa[v] > (getAresta(c, v).getPeso() + estimativa[c])) // serve para V ja visitados mas que possam ter estm. maior que a atual
                        {
                            estimativa[v] = (getAresta(c, v).getPeso() + estimativa[c]);
                            antecessor[v] = c;
                        }
                        else if(estimativa[v]==0)
                        {
                            estimativa[v] = (getAresta(c, v).getPeso() + estimativa[c]);
                            antecessor[v] = c;
                        }
                    }
                }
                for (int i = 0; i < getN(); i++)
                {
                    if (estimativa[i] < min && !fechados[i] && estimativa[i]!=0)
                    {
                        min = estimativa[i];
                        c = i;
                    }
                }
                fechados[c] = true;
                min = 1000000;
            }
            while (f != inicial)
            {
                getAresta(antecessor[f], f).setCor(System.Drawing.Color.YellowGreen);
                f = antecessor[f];
            }
            Custo += estimativa[destino];
        }
        public string getCustoCaminho()
        {
            return Custo;
        }

        public void completarGrafo() 
        {
            for (int i = 1; i <= getN(); i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (getAresta(i,j) == null)
                    {
                        if (getPesosAleatorios()) // Se o pesos aleatórios estiver marcado ele completa ja com eles
                        {
                            setAresta(i, j, aleatorio.Next(1, 100));
                        }
                        else
                            setAresta(i, j, 1);
                    }
                }
            }

            limparCores();
        }

        public bool isEuleriano()
        {
            int i;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    return false;

            }
            if (getN() != 0)
                return true;
            else
                return false;
        }

        public bool isUnicursal()
        {
            int i;
            int impar = 0;
            for (i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    impar++;

            }
            if (getN() != 0 && impar==2)
                return true;
            else
                return false;
        }

        public void largura(int v)
        {

            limparCores();

            visitado = new bool[getN()];
            Fila f = new Fila(matAdj.GetLength(0));

            f.enfileirar(v);
            visitado[v] = true;
            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (int i = 0; i < getN(); i++)
                {
                    if(getAresta(v, i) != null && !visitado[i])
                    {
                        getAresta(v, i).setCor(System.Drawing.Color.YellowGreen);
                        visitado[i] = true;
                        f.enfileirar(i);
                    }
                }
            }
        }

        public void numeroCromatico()
        {
            limparCores();

            visitado = new bool[getN()];
            Fila f = new Fila(getN());
            Color[] cor = { Color.Purple, Color.Green, Color.Blue, Color.Yellow, Color.DarkRed, Color.HotPink, Color.ForestGreen, Color.RosyBrown, Color.Silver, Color.Black };
            int maiorGrau = -1, v = 0,c=1;
            int[] corDoVertice = new int[getN()]; // auxiliar pra indicar o indice do vetor cor
            bool continua = true;
            numeroCromático = 0; 

            for (int i = 0; i < getN(); i++)//pega pelo v de maior grau
            {
               if(grau(i) > maiorGrau)
                {
                    maiorGrau = grau(i);
                    v = i;
                }
            }

            f.enfileirar(v);
            visitado[v] = true;
            corDoVertice[v] = 1;
            getVertice(v).setCor(cor[0]);
            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (int i = 0; i < getN(); i++)
                {
                    if (getAresta(v, i) != null && !visitado[i])
                    {
                        while (continua)
                        {
                            for(int j = 0; j < getN(); j++)
                            {
                                if(getAresta(i,j)!= null && corDoVertice[j]== c)
                                {
                                    j = getN();
                                    c++;
                                }
                                else if (j == getN() - 1)
                                {
                                    corDoVertice[i] = c;
                                    getVertice(i).setCor(cor[(c - 1)]);
                                    continua = false;
                                }
                            }
                        }
                        visitado[i] = true;
                        c = 1;
                        continua = true;
                        f.enfileirar(i);
                    }
                }
            }
            for (int i = 0; i < getN(); i++)
            {
                if (corDoVertice[i] > c )
                {
                    c = corDoVertice[i+1];
                }
            }
            numeroCromático = c;

        }
        public int getNumeroCromático()
        {
            return numeroCromático;
        }

        public String paresOrdenados()
        {
            string pares = "{ ";
            for (int i = 0; i <=getN(); i++)
            {
                for (int j = 0; j < getN(); j++)
                {
                    if (getAresta(i,j) != null)
                    {
                        pares += "(" + i + "," + j + ")" + ",";
                    }

                }
            }
            pares = pares.Substring(0,pares.Length-1) + "}";
            return pares;
        }
        public void profundidade(int v)
        {

            visitado[v] = true;
            for (int i = 0; i < getN(); i++)
            {
                if(getAresta(v, i) != null && ! visitado[i])
                {
                    getAresta(v, i).setCor(System.Drawing.Color.YellowGreen); // tem o System.Drawning pq não vi que ainda não tinha percebido a falta da Library
                    profundidade(i);
                }
            }

        }
        public void chamadaProfundidade(int v) // Precisou de método de chamada que o profundidade é recursivo e instanciar vetor direto nele ia dar problema
        {
            limparCores();
            visitado = new bool[getN()];
            profundidade(v);
        }

        public bool isArvore() // Usa o largura.
        {
            int v = 0;
            visitado = new bool[getN()];
            Fila f = new Fila(getN());
            int[] antecessor = new int[getN()];

            f.enfileirar(v);
            visitado[v] = true;
            antecessor[v] = v;
            while (!f.vazia())
            {
                v = f.desenfileirar();
                for (int i = 0; i < getN(); i++)
                {
                    if (getAresta(v,i) != null && !visitado[i])
                    {
                        visitado[i] = true;
                        f.enfileirar(i);
                        antecessor[i] = v;
                    }
                    else if (getAresta(v, i) != null && visitado[i] && antecessor[v] != i) // se o v visita um outro v ja visitado que não é seu pai, não é arvore
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
