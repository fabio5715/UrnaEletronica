using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using System.Media;

namespace UrnaEletrônica
{
    public partial class UrnaEletrônica : Form

    {
        private bool votoEmBranco = false;
        private SoundPlayer player1;
        private SoundPlayer player2;

        Dictionary<string, (string nome, string partido, string cargo, string vice, string foto, string fotoVice)> candidatos = new Dictionary<string, (string nome, string partido, string cargo, string vice, string foto, string fotoVice)>()
{
            { "13", ("Saci Pererê", "Saltadores do Folclore", "Prefeito", "Curupira", @"C:\Users\fabio\source\repos\UrnaEletrônica\Sacipere.png", @"C:\Users\fabio\source\repos\UrnaEletrônica\Curupira.png") },
            { "22", ("Mula Sem Cabeça", "Sem Direção", "Prefeito", "Boitatá", @"C:\Users\fabio\source\repos\UrnaEletrônica\Mulasemcabeca.png", @"C:\Users\fabio\source\repos\UrnaEletrônica\Boitata.png") },
            { "31", ("Lobisomem", "Lua Cheia", "Prefeito", "Boto Cor-de-Rosa", @"C:\Users\fabio\source\repos\UrnaEletrônica\Lobisomem.png", @"C:\Users\fabio\source\repos\UrnaEletrônica\BotoCordeRosa.png") },
            { "44", ("Caipora", "Montaria", "Prefeito", "Negrinho do Pastoreio", @"C:\Users\fabio\source\repos\UrnaEletrônica\Caipora.png", @"C:\Users\fabio\source\repos\UrnaEletrônica\Negrinho.png") },
            { "13001", ("Matinta Pereira", "Saltadores do Folclore", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\MatintaPereira.png", "") },
            { "13002", ("Boibumbá", "Saltadores do Folclore", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\Boibumba.png", "") },
            { "22001", ("Bicho-Papão", "Sem Direção", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\Bichopapao.png","") },
            { "22002", ("Comadre Fulozinha", "Sem Direção", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\ComadreFulozinha.png","") },
            { "31001", ("Cuca", "Lua Cheia", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\Cuca.png","") },
            { "31002", ("Iara-Seria", "Lua Cheia", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\Iara-sereia.png","") },
            { "44001", ("Mani-Mandioca", "Montaria", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\Manimandioca.png","") },
            { "44002", ("Vitoria-Regia", "Montaria", "Vereador", "", @"C:\Users\fabio\source\repos\UrnaEletrônica\VitoriaRegia.png","") },
        };
        public UrnaEletrônica()
        {
            InitializeComponent();
            painelCandidatos();
            painel_vereador.Visible = true;
            painel_prefeito.Visible = false;
            text_n1.Focus();
            painel_fim.Visible = false;
            player1 = new SoundPlayer(@"C:\Users\fabio\source\repos\UrnaEletrônica\confirma_1.wav");
            player2 = new SoundPlayer(@"C:\Users\fabio\source\repos\UrnaEletrônica\confirma_final.wav");
        }
        private string votoVereador = "";
        private string votoPrefeito = "";
        private void panel_teclado_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btn_1_Click(object sender, EventArgs e) { InserirNumero("1"); }
        private void btn_2_Click(object sender, EventArgs e) { InserirNumero("2"); }
        private void btn_3_Click(object sender, EventArgs e) { InserirNumero("3"); }
        private void btn_4_Click(object sender, EventArgs e) { InserirNumero("4"); }
        private void btn_5_Click(object sender, EventArgs e) { InserirNumero("5"); }
        private void btn_6_Click(object sender, EventArgs e) { InserirNumero("6"); }
        private void btn_7_Click(object sender, EventArgs e) { InserirNumero("7"); }
        private void btn_8_Click(object sender, EventArgs e) { InserirNumero("8"); }
        private void btn_9_Click(object sender, EventArgs e) { InserirNumero("9"); }
        private void btn_0_Click(object sender, EventArgs e) { InserirNumero("0"); }
        private void InserirNumero(string numero)
        {
            if (!votoEmBranco)
            {
                if (painel_vereador.Visible)
                {
                    if (text_n1.Text == "") text_n1.Text = numero;
                    else if (text_n2.Text == "") text_n2.Text = numero;
                    else if (text_n3.Text == "") text_n3.Text = numero;
                    else if (text_n4.Text == "") text_n4.Text = numero;
                    else if (text_n5.Text == "") text_n5.Text = numero;

                    if (text_n5.Text != "")
                    {
                        string codigoVereador = text_n1.Text + text_n2.Text + text_n3.Text + text_n4.Text + text_n5.Text;
                        VerifCandVereador(codigoVereador, "Vereador");
                    }
                }

                else if (painel_prefeito.Visible)
                {
                    if (text_p_n1.Text == "") text_p_n1.Text = numero;
                    else if (text_p_n2.Text == "") text_p_n2.Text = numero;

                    if (text_p_n2.Text != "")
                    {
                        string codigoPrefeito = text_p_n1.Text + text_p_n2.Text;
                        VerifCandPrefeito(codigoPrefeito, "Prefeito");
                    }
                }
            }
        }
        private void VerifCandVereador(string codigo, string cargoEsperado)
        {
            if (candidatos.ContainsKey(codigo))
            {
                var candidato = candidatos[codigo];

                if (cargoEsperado == "Vereador" && candidato.cargo == "Vereador")
                {
                    text_nome.Text = candidato.nome;
                    text_partido.Text = candidato.partido;
                    text_candidato.Text = candidato.cargo;

                    if (File.Exists(candidato.foto))
                    {
                        pic_vereador.Image = Image.FromFile(candidato.foto);
                    }
                    else
                    {
                        DefinirVotoNulo();
                    }
                }
            }
            else
            {
                DefinirVotoNulo();
            }
        }
        private void VerifCandPrefeito(string codigo, string cargoEsperado)
        {
            if (candidatos.ContainsKey(codigo))
            {
                var candidato = candidatos[codigo];

                if (cargoEsperado == "Prefeito" && candidato.cargo == "Prefeito")
                {
                    text_p_nome.Text = candidato.nome;
                    text_p_partido.Text = candidato.partido;
                    text_p_candidato.Text = candidato.cargo;
                    text_pic_p_nome.Text = candidato.cargo;

                    if (File.Exists(candidato.foto))
                    {
                        pic_prefeito.Image = Image.FromFile(candidato.foto);
                    }
                    else
                    {
                        DefinirVotoNuloPrefeito();
                    }
                    text_v_nome.Text = candidato.vice;
                    text_pic_v_nome.Text = "Vice-Prefeito";
                    if (File.Exists(candidato.fotoVice))
                    {
                        pic_vice.Image = Image.FromFile(candidato.fotoVice);
                    }
                    else
                    {
                        pic_vice.Image = null;
                    }
                }
            }
            else
            {
                DefinirVotoNuloPrefeito();
            }
        }
        private void DefinirVotoNulo()
        {
            text_nome.Text = "VOTO NULO";
            text_partido.Text = "";
            text_candidato.Text = "Vereador";
            pic_vereador.Image = null;
        }
        private void DefinirVotoNuloPrefeito()
        {
            text_p_nome.Text = "VOTO NULO";
            text_p_partido.Text = "";
            text_p_candidato.Text = "Prefeito";
            text_v_nome.Text = "";
            pic_prefeito.Image = null;
            pic_vice.Image = null;
        }
        private void btn_corrige_Click(object sender, EventArgs e)
        {
            if (painel_prefeito.Visible)
            {
                ResetarVoto();
                AparecerVotoPrefeito();
                votoEmBranco = false;
            }
            else if (painel_vereador.Visible)
            {
                ResetarVoto();
                AparecerVotoVereador();
                votoEmBranco = false;
            }
        }
        private void btn_branco_Click(object sender, EventArgs e)
        {
            if (painel_vereador.Visible)
            {
                ResetarVoto();
                text_nome.Text = "VOTO EM BRANCO";
                text_n1.Visible = false;
                text_n2.Visible = false;
                text_n3.Visible = false;
                text_n4.Visible = false;
                text_n5.Visible = false;
                lbl_2.Visible = false;
                lbl_3.Visible = false;
                lbl_4.Visible = false;
                votoEmBranco = true;
            }
            else if (painel_prefeito.Visible)
            {
                ResetarVoto();
                text_p_nome.Text = "VOTO EM BRANCO";
                text_p_n1.Clear();
                text_p_n2.Clear();
                text_p_partido.Clear();
                text_v_nome.Clear();
                pic_prefeito.Image = null;
                pic_vice.Image = null;
                text_p_n1.Visible = false;
                text_p_n2.Visible = false;
                lbl_p_2.Visible = false;
                lbl_p_3.Visible = false;
                lbl_p_4.Visible = false;
                lbl_p_5.Visible = false;
                votoEmBranco = true;
            }
        }
        private void AparecerVotoVereador()
        {
            text_n1.Visible = true;
            text_n2.Visible = true;
            text_n3.Visible = true;
            text_n4.Visible = true;
            text_n5.Visible = true;
            lbl_2.Visible = true;
            lbl_3.Visible = true;
            lbl_4.Visible = true;
            votoEmBranco = false;
        }
        private void AparecerVotoPrefeito()
        {
            text_p_n1.Visible = true;
            text_p_n2.Visible = true;
            lbl_p_2.Visible = true;
            lbl_p_3.Visible = true;
            lbl_p_4.Visible = true;
            lbl_p_5.Visible = true;
            text_pic_p_nome.Visible = true;
        }
        private void btn_confirma_Click(object sender, EventArgs e)
        {
            if (painel_vereador.Visible)
            {
                if (!string.IsNullOrEmpty(text_nome.Text))
                {
                    player1.Play();
                    Thread.Sleep(500);
                    votoVereador = text_n1.Text + text_n2.Text + text_n3.Text + text_n4.Text + text_n5.Text;
                    painel_vereador.Visible = false;
                    painel_prefeito.Visible = true;
                    text_nome.Clear();
                    text_candidato.Clear();
                    text_partido.Clear();
                    pic_prefeito.Image = null;
                    pic_vereador.Image = null;
                    text_p_nome.Clear();
                    text_p_candidato.Clear();
                    text_p_partido.Clear();
                    votoEmBranco = false;
                }
                else
                {
                    MessageBox.Show("Para CONFIRMAR é necessário digitar pelo meno o números do partido ou vatr em BRANCO");
                }
            }
            else if (painel_prefeito.Visible)
            {
                if (!string.IsNullOrEmpty(text_p_nome.Text))
                {
                    player2.Play();
                    Thread.Sleep(500);
                    votoPrefeito = text_p_n1.Text + text_p_n2.Text;
                    painel_prefeito.Visible = false;
                    painel_fim.Visible = true;
                    text_nome.Clear();
                    text_candidato.Clear();
                    text_partido.Clear();
                    pic_prefeito = null;
                    pic_vereador = null;
                    text_p_nome.Clear();
                    text_p_candidato.Clear();
                    text_p_partido.Clear();
                    votoEmBranco = false;
                }
                else
                {
                    MessageBox.Show("Para CONFIRMAR é necessário digitar pelo meno o números do partido ou vatr em BRANCO");
                }
            }
        }
        private void ResetarVoto()
        {
            text_n1.Clear();
            text_n2.Clear();
            text_n3.Clear();
            text_n4.Clear();
            text_n5.Clear();
            text_nome.Clear();
            text_partido.Clear();
            pic_vereador.Image = null;
            text_p_n1.Clear();
            text_p_n2.Clear();
            text_p_nome.Clear();
            text_p_partido.Clear();
            text_v_nome.Clear();
            pic_prefeito.Image = null;
            pic_vice.Image = null;
            text_pic_p_nome.Clear();
            text_pic_v_nome.Clear();
        }
        private void text_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_n1_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_n2_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_n3_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_n4_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_n5_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_partido_TextChanged(object sender, EventArgs e)
        {

        }

        private void pic_vereador_Click(object sender, EventArgs e)
        {

        }

        private void text_cand_foto1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pic_vice_Click(object sender, EventArgs e)
        {

        }

        private void text_cand_foto2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel_vereador_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_prefeito_Paint(object sender, PaintEventArgs e)
        {

        }

        private void text_p_candidato_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_p_n1_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_p_n2_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_p_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_p_partido_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_v_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_pic_p_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void text_pic_v_nome_TextChanged(object sender, EventArgs e)
        {

        }

        private void painel_voto_branco_Paint(object sender, PaintEventArgs e)
        {

        }

        private void text_b_candidato_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbl_data_Click(object sender, EventArgs e)
        {

        }


        private void painelCandidatos()
        {
            painel_cand.Visible = true;
            painel_13.Visible = false;
            painel_22.Visible = false;
            painel_31.Visible = false;
            painel_44.Visible = false;
        }

        private void btn_13_Click(object sender, EventArgs e)
        {
            painel_cand.Visible = false;
            painel_13.Visible = true;
            painel_22.Visible = false;
        }

        private void btn_22_Click(object sender, EventArgs e)
        {
            painel_cand.Visible = false;
            painel_13.Visible = false;
            painel_22.Visible = true;
        }
        private void btn_volta13_Click_1(object sender, EventArgs e)
        {
            painelCandidatos();
        }

        private void btn_volta22_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }

        private void btn_31_Click(object sender, EventArgs e)
        {
            painel_31.Visible = true;
            painel_22.Visible = false;
            painel_13.Visible = false;
            painel_cand.Visible = false;

        }

        private void btn_volta31_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }

        private void btn_volta44_Click(object sender, EventArgs e)
        {
            painelCandidatos();
        }

        private void btn_44_Click(object sender, EventArgs e)
        {
            painel_44.Visible = true;
            painel_31.Visible = false;
            painel_22.Visible = false;
            painel_13.Visible = false;
            painel_cand.Visible = false;
        }
    }
}
