using System;
using System.Collections.Generic;

namespace CusomerCareModule.DAL;

public partial class ComplaintHistory
{
    public int Id { get; set; }

    public int? ComplaintId { get; set; }

    public int? CurrentStatus { get; set; }

    public string? Description { get; set; }

    public DateTime? ActionDate { get; set; }

    public int? UserId { get; set; }

    public virtual Complaint? Complaint { get; set; }

    public virtual User? User { get; set; }
}
