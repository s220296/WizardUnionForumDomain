﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WizardUnion.Messaging;

namespace WU_Test;

public static class DataSubmission
{
    // Record(Wizard), Record(Place), etc...
    public static void RecordTextMessage(TextMessage _message, IMessageSender _sender, IMessageReceiver _receiver)
    {
        if (_message is null || _sender is null || _receiver is null)
            return;

        IDTypeItem sender = (IDTypeItem)_sender.GetSender();
        IDTypeItem receiver = (IDTypeItem)_receiver.GetReceiver();
    }
}
