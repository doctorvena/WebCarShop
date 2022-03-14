
"use strict"

//const { signalR } = require("./signalr/dist/browser/signalr");

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//disabelind button
$("#btnSend").attr("disabled", true);

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt").replace(/>/g, "&gt;");
    var encodeMsg = user + " : " + msg;
    $("#messageList").prepend("<li>" + encodeMsg + "</li>");

});

connection.start().then(function () {
    $("#btnSend").attr("disabled", false);
}).catch(function (err) {
    alert(err.toString());
})

$("#btnSend").on("click", function (event) {
    var user = $("#txtUserName").val();
    var message = $("#txtMessage").val();

    if (user != "" && message != "") {
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return alert(err.toString())
        });
        event.preventDefault();
        $("#txtMessage").Value = "";
        ClearFields();
    }
});

function ClearFields() {
    document.getElementById("txtMessage").value = "";
}

//drugiiiiiiiiiiiiiiiiiiiii

class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
    addMessageToChat(document.getElementById('#messageText'));
    addMessageToChat(textInput);
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let when = new Date();
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker" : "container";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-left" : "time-right";
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    chat.appendChild(container);
}
