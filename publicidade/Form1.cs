using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace publicidade
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        
        int idServico;

        private void btnCadastarPubli_Click(object sender, EventArgs e)
        {
            //criando o objeto de conexão com banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server=turmassenacsantos.mssql.somee.com;Database=turmassenacsantos;User Id=senacclovis_SQLLogin_1;Password=n1642mlxmm;";

            //criando o objeto de comando de SQL para enviar instruções para o banco de dados
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; //ligando o comando a conexão que configurada acima
            cmd.CommandText = "piServico";
            cmd.CommandType = CommandType.StoredProcedure; //definindo que o comando é um procedimento

            //vincular os campos do formulários aos parâmetros do procedimento
            cmd.Parameters.AddWithValue("nome", txtNome.Text);
            cmd.Parameters.AddWithValue("descricao", txtDescricao.Text);
            cmd.Parameters.AddWithValue("duracao", txtDuracao.Text);
            cmd.Parameters.AddWithValue("valor", txtValor.Text);


            connection.Open();
            cmd.ExecuteNonQuery(); //executar para comandos que não possuem retorno de dados (INSERT, UPDATE e DELETE)
            connection.Close();

            MessageBox.Show("Serviço cadastrado com sucesso!");
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Deseja realmente atualizar?", "Aviso!", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                //criando o objeto de conexão com banco de dados
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Server=turmassenacsantos.mssql.somee.com;Database=turmassenacsantos;User Id=senacclovis_SQLLogin_1;Password=n1642mlxmm;";

                //criando o objeto de comando de SQL para enviar instruções para o banco de dados
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection; //ligando o comando a conexão que configurada acima
                cmd.CommandText = "puServico";
                cmd.CommandType = CommandType.StoredProcedure; //definindo que o comando é um procedimento

                //vincular os campos do formulários aos parâmetros do procedimento
                cmd.Parameters.AddWithValue("nome", txtNome.Text);
                cmd.Parameters.AddWithValue("descricao", txtDescricao.Text);
                cmd.Parameters.AddWithValue("duracao", txtDuracao.Text);
                cmd.Parameters.AddWithValue("valor", txtValor.Text);
                cmd.Parameters.AddWithValue("idServico", txtID.Text);

                connection.Open();
                cmd.ExecuteNonQuery(); //executar para comandos que não possuem retorno de dados (INSERT, UPDATE e DELETE)
                connection.Close();

                MessageBox.Show("Serviço atualizado com sucesso!");
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buscar()
        {
            //criando o objeto de conexão com banco de dados
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = "Server=turmassenacsantos.mssql.somee.com;Database=turmassenacsantos;User Id=senacclovis_SQLLogin_1;Password=n1642mlxmm;";

            //criando o objeto de comando de SQL para enviar instruções para o banco de dados
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; //ligando o comando a conexão que configurada acima
            cmd.CommandText = "psServico";
            cmd.CommandType = CommandType.StoredProcedure; //definindo que o comando é um procedimento

            cmd.Parameters.AddWithValue("idServico", txtID.Text);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(); //executando o comando de busca no SQL e armazenando o resultado da busca em um leitor (matriz)

            if (reader.HasRows) //se o leitor tem linhas de dados
            {
                reader.Read(); //ler a próxima linha de dados

                //tipos de GET em C#

                //GetString = textos (VARCHAR)
                //GetDecimal = valores decimais (DECIMAL)
                //GetInt32 = números inteiros (INT)
                //GetInt64 = números inteiros (BIGINT)
                //GetDateTime = data (DATE, TIME ou DATETIME)

                txtNome.Text = reader.GetString(1);
                txtDescricao.Text = reader.GetString(2);
                txtDuracao.Text = reader.GetInt32(3).ToString();
                txtValor.Text = reader.GetDecimal(4).ToString(".");

            }
            else
            {
                MessageBox.Show("Serviço não encontrado!");
            }

            connection.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (txtID.TextLength > 0)
            {
                buscar();
            }
        }

        


        private void btnDeletar_Click(object sender, EventArgs e)

        {
            

            DialogResult result = MessageBox.Show("Deseja realmente excluir?", "Aviso", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                //criando o objeto de conexão com banco de dados
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = "Server=turmassenacsantos.mssql.somee.com;Database=turmassenacsantos;User Id=senacclovis_SQLLogin_1;Password=n1642mlxmm;";

                //criando o objeto de comando de SQL para enviar instruções para o banco de dados
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection; //ligando o comando a conexão que configurada acima
                cmd.CommandText = "pdServico";
                cmd.CommandType = CommandType.StoredProcedure; //definindo que o comando é um procedimento

                //vincular os campos do formulários aos parâmetros do procedimento
                cmd.Parameters.AddWithValue("idServico", txtID.Text);

                connection.Open();
                cmd.ExecuteNonQuery(); //executar para comandos que não possuem retorno de dados (INSERT, UPDATE e DELETE)
                connection.Close();

                MessageBox.Show("Serviço excluído com sucesso!");

                
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
