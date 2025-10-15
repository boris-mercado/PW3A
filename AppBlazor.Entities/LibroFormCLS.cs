using System.ComponentModel.DataAnnotations;

namespace AppBlazor.Entities
{
    public class LibroFormCLS
    {
        [Required (ErrorMessage = "El id es requerido")]
        [Range (0, int.MaxValue, ErrorMessage ="El valor minimo es 1")]
        public int idLibro { get; set; }


        [Required (ErrorMessage ="El titulo es requerido")]
        [MaxLength(100, ErrorMessage ="La longitud maxima es de 100 caracteres")]
        public string titulo { get; set; } = null!;


        [Required (ErrorMessage ="El resumen es requerido")]
        [MinLength(5, ErrorMessage = "La longitud minima es de 20 caracteres")]
        public string resumen { get; set; } = null!;

        //modificaciones
        public int hoja { get; set; }

        //para el tipo de libro
        [Range (1, int.MaxValue,ErrorMessage = "Debe selecionar un tipo de libro")]
        public int idtipolibro { get; set; }
        // imagen
        public byte[]? image { get; set; }
        //archivos
        public byte[]? archivo { get; set; }
        public string nombrearchivo { get; set; } = null!;

        public int idautor { get; set; }
    }
}
