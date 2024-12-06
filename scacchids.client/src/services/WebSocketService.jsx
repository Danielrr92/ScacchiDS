class WebSocketService {
    constructor() {
        this.socket = null;
        this.reconnectInterval = 5000; // Tentativo di riconnessione ogni 5 secondi
        this.isConnected = false;
        this.listeners = [];
    }

    connect(url) {
        return new Promise((resolve, reject) => {
            if (this.socket && this.isConnected) {
                return resolve({ success: true }); // Se già connesso, restituisci successo
            }

            this.socket = new WebSocket(url);

            this.socket.onopen = () => {
                console.log("WebSocket connected");
                this.isConnected = true;
                this.listeners.forEach((listener) => listener("connected"));
                resolve({ success: true });  // Risolvi la promessa con un valore indicante il successo
            };

            this.socket.onmessage = (event) => {
                const message = JSON.parse(event.data);
                console.log("Message received:", message);
                this.listeners.forEach((listener) => listener(message));
            };

            this.socket.onclose = () => {
                console.warn("WebSocket disconnected");
                this.isConnected = false;
                this.listeners.forEach((listener) => listener("disconnected"));
                reject(new Error("WebSocket disconnected"));  // Rifiuta la promessa se la connessione si chiude
            };

            this.socket.onerror = (error) => {
                console.error("WebSocket error:", error);
                reject(error);  // Rifiuta la promessa in caso di errore
            };
        });
    }



    reconnect(url) {
        if (!this.isConnected) {
            console.log("Attempting to reconnect...");
            this.connect(url);
        }
    }

    send(data) {
        if (this.socket && this.isConnected) {
            console.log("Entrato nel metodo send del WebSocketService. Data: ");
            console.log(data);
            this.socket.send(JSON.stringify(data));
        } else {
            console.error("WebSocket is not connected");
        }
    }

    addListener(callback) {
        this.listeners.push(callback);
    }

    removeListener(callback) {
        this.listeners = this.listeners.filter((listener) => listener !== callback);
    }
}

const webSocketService = new WebSocketService();

export default webSocketService;
