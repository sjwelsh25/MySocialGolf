using System;
 
namespace MySocialGolf.DtoModel
{
    /// <summary>
    /// Represents a User.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class _UserDto: BaseDto 
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public Guid UserGuid { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public decimal? CurrentHandicap { get; set; }
        public DateTime? CurrentHandicapCalcdOn { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public int? StatusId { get; set; }
    }
}      
