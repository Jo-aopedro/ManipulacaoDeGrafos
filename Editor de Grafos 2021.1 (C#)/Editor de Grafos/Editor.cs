using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();            
        }

        #region Botoes de Algoritmo do Menu
        private void BtParesOrd_Click(object sender, EventArgs e)
        {
            MessageBox.Show(g.paresOrdenados(), "Os pares ordenados do grafo são: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoEuleriano_Click(object sender, EventArgs e)
        {
            if(g.isEuleriano())
                MessageBox.Show("O grafo e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Euleriano!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoUnicursal_Click(object sender, EventArgs e)
        {
            if (g.isUnicursal())
                MessageBox.Show("O grafo e Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não e Unicursal!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buscaEmProfundidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(g.getVerticeMarcado() == null)
            {
                g.chamadaProfundidade(0);
            }
            else
                g.chamadaProfundidade(g.getVerticeMarcado().getNum());
            
        }

        private void buscaEmLarguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g.getVerticeMarcado() == null)
            {
                g.largura(0);
            }
            else
                g.largura(g.getVerticeMarcado().getNum());
            

        }

        private void éArvoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g.isArvore())
                MessageBox.Show("O grafo é Uma Árvore!", "Mensagem", MessageBoxButtons.OK);
            else
                MessageBox.Show("O grafo não é Uma Árvore!", "Mensagem", MessageBoxButtons.OK);

        }

        private void caminhoMínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g.getVerticeMarcado() == null)
            {
                MessageBox.Show("Selecione o vértice de origem antes de iniciar.", "Atenção", MessageBoxButtons.OK);
            }
            else
            {
                int v = Convert.ToInt32(Interaction.InputBox("Digite o número do vertice de destino (ex: vértice v1 = 1): "));
                g.caminhoMinimo(g.getVerticeMarcado().getNum(), v - 1);

                MessageBox.Show(g.getCustoCaminho(), "O custo do caminho é:", MessageBoxButtons.OK);
            }

        }

        private void arvoreGeradoraMinimaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (g.getVerticeMarcado() == null)
            {
                g.AGM(0);
                MessageBox.Show(g.getCustoCaminho(), "O custo da AGM é:", MessageBoxButtons.OK);
            }
            else
            {
                g.AGM(g.getVerticeMarcado().getNum());
                MessageBox.Show(g.getCustoCaminho(), "O custo da AGM é:", MessageBoxButtons.OK);
            }
        }

        private void númeroCromáticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.numeroCromatico();
            MessageBox.Show(Convert.ToString(g.getNumeroCromático()), "O nº Cromático é:", MessageBoxButtons.OK);
        }

        #endregion --------------------------------------------------------------------------------------------------

        #region botoes restantes do menu

        private void BtNovo_Click(object sender, EventArgs e)
        {
            g.limpar();
        }

        private void BtAbrir_Click(object sender, EventArgs e)
        {
            if(OPFile.ShowDialog() == DialogResult.OK)
            {
                g.abrirArquivo(OPFile.FileName);
                g.Refresh();
            }
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if(SVFile.ShowDialog() == DialogResult.OK)
            {
                g.SalvarArquivo(SVFile.FileName);
            }
        }

        private void BtSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtPeso_Click(object sender, EventArgs e)
        {
            if(BtPeso.Checked)
            {
                BtPeso.Checked = false;
                g.setExibirPesos(false);

            }
            else
            {
                BtPeso.Checked = true;
                g.setExibirPesos(true);
            }
            g.Refresh();
        }

        private void BtPesoAleatorio_Click(object sender, EventArgs e)
        {
            if(BtPesoAleatorio.Checked)
            {
                BtPesoAleatorio.Checked = false;
                g.setPesosAleatorios(false);
            }
            else
            {
                BtPesoAleatorio.Checked = true;
                g.setPesosAleatorios(true);
            }
        }

        private void BtSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Editor de Grafos - 2021/1\n\nDesenvolvido por:\nNatan Macedo de Magalhaes\nVirgilio Borges de Oliveira\nJoão Pedro Martins de Moura \n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.", "Sobre o Editor de Grafos...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion --------------------------------------------------------------------------------------------------

        private void completarGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.completarGrafo();
        }

        private void ferramentasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void algoritmosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void limparTelaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.limparCores();
        }
    }
}
