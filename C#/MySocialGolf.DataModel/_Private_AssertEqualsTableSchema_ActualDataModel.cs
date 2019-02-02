using System;
 
namespace MySocialGolf.DataModel
{
    /// <summary>
    /// Represents a Private_AssertEqualsTableSchema_Actual.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _Private_AssertEqualsTableSchema_ActualDataModel: BaseDataModel 
    {
        public string name { get; set; }
        public int? RANK(column_id) { get; set; }
        public string system_type_id { get; set; }
        public string user_type_id { get; set; }
        public Int16? max_length { get; set; }
        public byte? precision { get; set; }
        public byte? scale { get; set; }
        public string collation_name { get; set; }
        public bool? is_nullable { get; set; }
        public bool? is_identity { get; set; }
    }
}      
