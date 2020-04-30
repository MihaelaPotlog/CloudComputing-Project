document.addEventListener('DOMContentLoaded', function () {

    var userMail = document.getElementById('usermail');
    var username = userMail.innerHTML.split("#")[1];
    username = username.split("!")[0];

    // Set initial focus to message input box.
    var messageInput = document.getElementById('message');
    messageInput.focus();

    function createMessageEntry(encodedName, encodedMsg) {
        var entry = document.createElement('div');
        entry.classList.add("message-entry");
        if (encodedName === "_SYSTEM_") {
            entry.innerHTML = encodedMsg;
            entry.classList.add("text-center");
            entry.classList.add("system-message");
        } else if (encodedName === username) {
            entry.innerHTML = `<div class="message-avatar pull-right">${encodedName}</div>` +
                `<div class="message-content pull-right">${encodedMsg}<div>`;
        } else {
            entry.innerHTML = `<div class="message-avatar pull-left">${encodedName}</div>` +
                `<div class="message-content pull-left">${encodedMsg}<div>`;
        }
        return entry;
    }

    function bindConnectionMessage(connection) {
        var messageCallback = function (name, message) {
            if (!message) return;
            // Html encode display name and message.
            var encodedName = name;
            var encodedMsg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            var messageEntry = createMessageEntry(encodedName, encodedMsg);

            var messageBox = document.getElementById('messages');
            messageBox.appendChild(messageEntry);
            messageBox.scrollTop = messageBox.scrollHeight;

        };


        // callable functions from server
        connection.on('broadcastMessage', messageCallback);
        connection.on('badWords', () => window.alert("You shall not use this language!"));

        connection.onclose(onConnectionError);
    }

    function onConnected(connection) {
        console.log('Connection with the chathub started');
        connection.send('broadcastMessage', '_SYSTEM_', username + ' JOINED');
        document.getElementById('sendmessage').addEventListener('click', function (event) {

            if (messageInput.value) {
                connection.send('broadcastMessage', username, messageInput.value);
            }


            messageInput.value = '';
            messageInput.focus();
            event.preventDefault();
        });
        document.getElementById('message').addEventListener('keypress', function (event) {
            if (event.keyCode === 13) {
                event.preventDefault();
                document.getElementById('sendmessage').click();
                return false;
            }
        }
        );

    }

    function onConnectionError(error) {
        if (error && error.message) {
            console.error(error.message);
        }
        var modal = document.getElementById('myModal');
        modal.classList.add('in');
        modal.style = 'display: block;';
    }

    var connection = new signalR.HubConnectionBuilder()
        .withUrl('/chat')
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



document.getElementById("subscribe").addEventListener("click", () => {
    var userMail = document.getElementById('usermail');
    var username = userMail.innerHTML.split("#")[1];
    username = username.split("!")[0];
    var body = { email: username };
    fetch("https://restaurantedb.azurewebsites.net/api/HttpTrigger3", {
        method: "POST", headers: {
            'Content-Type': 'application/json'

        }, body: JSON.stringify(body)
    });
});