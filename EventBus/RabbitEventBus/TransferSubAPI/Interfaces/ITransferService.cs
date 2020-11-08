using TransferSubAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransferSubAPI.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<TransferLog> GetTransferLogs();
    }
}

