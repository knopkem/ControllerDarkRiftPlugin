using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///     The tags for messages between the server and the client.
/// </summary>
static class MessageTags
{
    public static readonly ushort Initialize = 0; // send by controller on start
    public static readonly ushort Input = 1; // send by controller as input, received by game
    public static readonly ushort Pair = 2; // send by game at start
}