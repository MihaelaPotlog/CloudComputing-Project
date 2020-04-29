#pragma checksum "D:\CloudComputing\TemaCCAzure\TemaAzure2\Views\Home\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c5fb2ea9dab9d4a648785941feb5281de09c96fa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Chat), @"mvc.1.0.view", @"/Views/Home/Chat.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\CloudComputing\TemaCCAzure\TemaAzure2\Views\_ViewImports.cshtml"
using TemaAzure2;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\CloudComputing\TemaCCAzure\TemaAzure2\Views\_ViewImports.cshtml"
using TemaAzure2.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c5fb2ea9dab9d4a648785941feb5281de09c96fa", @"/Views/Home/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0a61b8fee5ab57602995636da8badf54e700054", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Chat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "D:\CloudComputing\TemaCCAzure\TemaAzure2\Views\Home\Chat.cshtml"
  
    ViewData["Title"] = "Chat";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Chat</h1>

<div class=""container"" style=""height: calc(100% - 110px);"">
    <div id=""messages"" style=""background-color: whitesmoke; ""></div>
    <div style=""width: 100%; border-left-style: ridge; border-right-style: ridge;"">
        <textarea id=""message""
                  style=""width: 100%; padding: 5px 10px; border-style: hidden;""
                  placeholder=""Type message and press Enter to send...""></textarea>
    </div>
    <div style=""overflow: auto; border-style: ridge; border-top-style: hidden;"">
        <button class=""btn-warning pull-right"" id=""echo"">Echo</button>
        <button class=""btn-success pull-right"" id=""sendmessage"">Send</button>
    </div>
</div>
<div class=""modal alert alert-danger fade"" id=""myModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <div>Connection Error...</div>
                <div><strong style=""");
            WriteLiteral(@"font-size: 1.5em;"">Hit Refresh/F5</strong> to rejoin. ;)</div>
            </div>
        </div>
    </div>
</div>
<!--Script references. -->
<!--Reference the SignalR library. -->
<script type=""text/javascript"" src=""~\lib\signalr\signalr.min.js""></script>

<!--Add script to update the page and send messages.-->
<script type=""text/javascript"">
        document.addEventListener('DOMContentLoaded', function () {

            function generateRandomName() {
                return Math.random().toString(36).substring(2, 10);
            }

            // Get the user name and store it to prepend to messages.
            var username = generateRandomName();
            var promptMessage = 'Enter your name:';
            do {
                username = prompt(promptMessage, username);
                if (!username || username.startsWith('_') || username.indexOf('<') > -1 || username.indexOf('>') > -1) {
                    username = '';
                    promptMessage = 'Invalid input. En");
            WriteLiteral(@"ter your name:';
                }
            } while(!username)

            // Set initial focus to message input box.
            var messageInput = document.getElementById('message');
            messageInput.focus();

            function createMessageEntry(encodedName, encodedMsg) {
                var entry = document.createElement('div');
                entry.classList.add(""message-entry"");
                if (encodedName === ""_SYSTEM_"") {
                    entry.innerHTML = encodedMsg;
                    entry.classList.add(""text-center"");
                    entry.classList.add(""system-message"");
                } else if (encodedName === ""_BROADCAST_"") {
                    entry.classList.add(""text-center"");
                    entry.innerHTML = `<div class=""text-center broadcast-message"">${encodedMsg}</div>`;
                } else if (encodedName === username) {
                    entry.innerHTML = `<div class=""message-avatar pull-right"">${encodedName}</div>` +
        ");
            WriteLiteral(@"                `<div class=""message-content pull-right"">${encodedMsg}<div>`;
                } else {
                    entry.innerHTML = `<div class=""message-avatar pull-left"">${encodedName}</div>` +
                        `<div class=""message-content pull-left"">${encodedMsg}<div>`;
                }
                return entry;
            }

            function bindConnectionMessage(connection) {
                var messageCallback = function(name, message) {
                    if (!message) return;
                    // Html encode display name and message.
                    var encodedName = name;
                    var encodedMsg = message.replace(/&/g, ""&amp;"").replace(/</g, ""&lt;"").replace(/>/g, ""&gt;"");
                    var messageEntry = createMessageEntry(encodedName, encodedMsg);

                    var messageBox = document.getElementById('messages');
                    messageBox.appendChild(messageEntry);
                    messageBox.scrollTop = messageBox.sc");
            WriteLiteral(@"rollHeight;
                };
                // Create a function that the hub can call to broadcast messages.
                connection.on('broadcastMessage', messageCallback);
                connection.on('echo', messageCallback);
                connection.onclose(onConnectionError);
            }

            function onConnected(connection) {
                console.log('connection started');
                connection.send('broadcastMessage', '_SYSTEM_', username + ' JOINED');
                document.getElementById('sendmessage').addEventListener('click', function (event) {
                    // Call the broadcastMessage method on the hub.
                    if (messageInput.value) {
                        connection.send('broadcastMessage', username, messageInput.value);
                    }

                    // Clear text box and reset focus for next comment.
                    messageInput.value = '';
                    messageInput.focus();
                    event");
            WriteLiteral(@".preventDefault();
                });
                document.getElementById('message').addEventListener('keypress', function (event) {
                    if (event.keyCode === 13) {
                        event.preventDefault();
                        document.getElementById('sendmessage').click();
                        return false;
                    }
                });
                document.getElementById('echo').addEventListener('click', function (event) {
                    // Call the echo method on the hub.
                    connection.send('echo', username, messageInput.value);

                    // Clear text box and reset focus for next comment.
                    messageInput.value = '';
                    messageInput.focus();
                    event.preventDefault();
                });
            }

            function onConnectionError(error) {
                if (error && error.message) {
                    console.error(error.message);
        ");
            WriteLiteral(@"        }
                var modal = document.getElementById('myModal');
                modal.classList.add('in');
                modal.style = 'display: block;';
            }

            var connection = new signalR.HubConnectionBuilder()
                .withUrl('https://backendfortemacc.azurewebsites.net/chat')
                                        .build();
            bindConnectionMessage(connection);
            connection.start()
                .then(function () {
                    onConnected(connection);
                })
                .catch(function (error) {
                    console.error(error.message);
                });
        });
</script>

");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591