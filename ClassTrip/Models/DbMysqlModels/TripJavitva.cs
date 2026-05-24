using System;
using System.Collections.Generic;

namespace ClassTrip.Models.DbMysqlModels;

public partial class TripJavitva
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Class { get; set; }

    public string Destination { get; set; }

    public int PaidAmount { get; set; }
}
