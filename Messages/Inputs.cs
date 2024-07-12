using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages;

public record struct AddTicketInput(string Id, string Seat, string Gate);