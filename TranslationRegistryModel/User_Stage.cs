//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TranslationRegistryModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Stage
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int StageId { get; set; }
        public int UserId { get; set; }
        public Nullable<int> DocFileId { get; set; }
        public System.DateTime Date { get; set; }
        public string Comment { get; set; }
    
        public virtual DocStage Stage { get; set; }
        public virtual User User { get; set; }
        public virtual DocFile DocFile { get; set; }
    }
}
