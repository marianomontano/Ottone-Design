//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PRODUCTO
    {
        public int ID { get; set; }
        [Display(Name = "Código de producto")]
        public string CODIGO_PRODUCTO { get; set; }
        [Display(Name = "Nombre")]
        public string NOMBRE { get; set; }
        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }
        [Display(Name = "Precio")]
        public Nullable<float> PRECIO { get; set; }
        [Display(Name = "Material")]
        public string MATERIAL { get; set; }
        [Display(Name = "Categoría")]
        public int CATEGORIA { get; set; }
        [Display(Name = "Subcategoría")]
        public int SUBCATEGORIA { get; set; }
        [Display(Name = "URL de la foto")]
        public string DIRECCION_FOTO { get; set; }

        [Display(Name = "Categoría")]
        public virtual CATEGORIA CATEGORIA1 { get; set; }
        [Display(Name = "Subcategoría")]
        public virtual SUBCATEGORIA SUBCATEGORIA1 { get; set; }
    }
}