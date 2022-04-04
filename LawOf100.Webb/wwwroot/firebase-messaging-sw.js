importScripts('https://www.gstatic.com/firebasejs/8.2.0/firebase-app.js');
importScripts('https://www.gstatic.com/firebasejs/8.2.0/firebase-messaging.js');

const firebaseConfig = {
    apiKey: "AIzaSyBDqWSzVu3i79VyPqTr4xP0DzTeHMwILtQ",
    authDomain: "law-of-100.firebaseapp.com",
    projectId: "law-of-100",
    storageBucket: "law-of-100.appspot.com",
    messagingSenderId: "602819901577",
    appId: "1:602819901577:web:3192c976719b4947232e58",
    measurementId: "G-MZTDQZE90E"
};

firebase.initializeApp(firebaseConfig);

const messaging = firebase.messaging();
