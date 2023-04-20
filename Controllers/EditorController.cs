using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assessment4_Solution.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EditorController : ControllerBase
    {
        private readonly ILogger<EditorController> _logger;
        IConfiguration _configuration;
        SqlConnection sqlConnection;


        public EditorController(ILogger<EditorController> logger, IConfiguration configuration) { 
            _logger = logger;
            _configuration = configuration;
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("Editor"));

        }

        [HttpGet("get")]
        public IEnumerable<Editor> GetDocuments()
        {
            List<Editor> editors = new();
            sqlConnection.Open();
            SqlCommand getEditorList = new SqlCommand("select * from document", sqlConnection);
            SqlDataReader reader = getEditorList.ExecuteReader();

            while (reader.Read())
            {
                Editor editor = new() 
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Description = reader.GetString("description")
                };
                

                editors.Add(editor);
            }

            sqlConnection.Close();

            return editors;
        }

        [HttpPost("add")]
        public string AddEditor([FromBody] AddParams editor)
        {

            string query = "INSERT INTO document VALUES (@Title, @Description)";
            sqlConnection.Open();

            SqlCommand createEditor = new SqlCommand(query, sqlConnection);

            createEditor.Parameters.AddWithValue("@Title", editor.title);
            createEditor.Parameters.AddWithValue("@Description", editor.description);

            createEditor.ExecuteNonQuery();

            sqlConnection.Close();

            return "Added Successfully!";
        }

        [HttpPost("update")]

        public string UpdateEditor([FromBody] AddParams editor)
        {
            string updateStatement = "UPDATE document SET Title = @Title, Description = @Description WHERE Id = @Id";
            sqlConnection.Open();
            SqlCommand command = new SqlCommand(updateStatement, sqlConnection);
            command.Parameters.AddWithValue("@Title", editor.title);
            command.Parameters.AddWithValue("@Description", editor.description);
            command.Parameters.AddWithValue("@Id", editor.id);

            command.ExecuteNonQuery();

            sqlConnection.Close();

            return "Updated Successfully!";
        }

        [HttpDelete("delete")]

        public string DeleteEditor([FromBody] int id)
        {
            string deleteStatement = "DELETE FROM document WHERE Id = @Id";

            sqlConnection.Open();
            SqlCommand command = new SqlCommand(deleteStatement, sqlConnection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();

            sqlConnection.Close();

            return "Deleted Successfully!";

        }

    }
}
