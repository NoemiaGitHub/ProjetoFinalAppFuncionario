using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace AppFuncionario
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string nome { get; set; }
        public string turno { get; set; }
        public string data_nascimento { get; set; }
        public string matricula { get; set; }

        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\Downloads\\AppFuncionario\\DbFuncionario.mdf;Integrated Security=True");

        public List<Funcionario> Listafuncionario()
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM Funcionario";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Funcionario func = new Funcionario();
                func.Id = (int)dr["Id"];
                func.nome = dr["nome"].ToString();
                func.turno = dr["turno"].ToString();
                func.data_nascimento = dr["data_nascimento"].ToString();
                func.matricula = dr["matricula"].ToString();
                li.Add(func);
            }
            dr.Close();
            con.Close();
            return li;
        }

        public void Inserir(string nome, string turno, string data_nascimento, string matricula)
        {
            try
            {
                string sql = "INSERT INTO Funcionario(nome,turno,data_nascimento,matricula) VALUES ('"+nome+"','"+turno+"','"+data_nascimento+"','"+matricula+"')";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public void Localizar(int Id)
        {
            try
            {
                string sql = "SELECT * FROM Funcionario WHERE Id='" + Id + "'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    nome = dr["nome"].ToString();
                    turno = dr["turno"].ToString();
                    data_nascimento = dr["data_nascimento"].ToString();
                    matricula = dr["matricula"].ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public void Atualizar(int Id, string nome, string turno, string data_nascimento, string matricula)
        {
            try
            {
                string sql = "UPDATE Funcionario SET nome='"+nome+"',turno='"+turno+"',data_nascimento='"+data_nascimento+"',matricula='"+matricula+"' WHERE Id='"+Id+"'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public void Excluir(int Id)
        {
            try
            {
                string sql = "DELETE FROM Funcionario WHERE Id='"+Id+"'";
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
        }

        public bool RegistroRepetido(string matricula)
        {
            string sql = "SELECT * FROM Funcionario WHERE matricula='"+matricula+"'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }

        public bool ExisteId(int Id)
        {
            string sql = "SELECT * FROM Funcionario WHERE Id='" + Id + "'";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            var result = cmd.ExecuteScalar();
            if (result != null)
            {
                return (int)result > 0;
            }
            con.Close();
            return false;
        }

        public bool Aniversario(string data)
        {
            string sql = "SELECT data_nascimento FROM Funcionario";
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            var dataniver = data;
            string data1 = dataniver.Substring(0, 2);
            string data2 = dataniver.Substring(3, 2);
            while (dr.Read())
            {
                var datanasc = dr["data_nascimento"].ToString();
                string datan1 = datanasc.Substring(0, 2);
                string datan2 = datanasc.Substring(3, 2);
                if(data1.ToString() == datan1.ToString() && data2.ToString() == datan2.ToString())
                {
                    return true;
                }
            }
            dr.Close();
            con.Close();
            return false;
        }

        public List <Funcionario> ListaAniversariantes() 
        {
            List<Funcionario> li = new List<Funcionario>();
            string sql = "SELECT * FROM Funcionario";
            if(con.State == ConnectionState.Open)
            { 
                con.Close();
            }
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            var dataniver = DateTime.Now.ToString("dd/MM/yyyy");
            string data1 = dataniver.Substring (0, 2);
            string data2 = dataniver.Substring (3, 2);
            while (dr.Read()) 
            {
                Funcionario func = new Funcionario();
                var datanasc = dr["data_nascimento"].ToString();
                string datan1 = datanasc.Substring (0, 2);
                string datan2 = datanasc.Substring (3, 2);
                if(data1.ToString() == datan1.ToString() && data2.ToString() ==datan2.ToString())
                {
                    func.Id = (int)dr["ID"];
                    func.nome = dr["nome"].ToString();
                    func.turno = dr["turno"].ToString();
                    func.data_nascimento = dr["data_nascimento"].ToString();
                    func.matricula = dr["matricula"].ToString();
                    li.Add(func);
                }

            }
            dr.Close();
            con.Close();
            return li;
        }

    }
}
