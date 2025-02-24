document.addEventListener("DOMContentLoaded", function () {
    if (typeof Chart === "undefined") {
        console.error("Chart.js n'est pas chargé correctement !");
        return;
    }

    const barCanvas = document.getElementById("barCanvas");
    const pieCanvas = document.getElementById("pieCanvas");
    const lineCanvas = document.getElementById("lineCanvas");
    const radarCanvas = document.getElementById("radarCanvas");
    const doughnutCanvas = document.getElementById("doughnutCanvas");



    if (!barCanvas || !pieCanvas || !lineCanvas || !radarCanvas || !doughnutCanvas  ) {
        console.error("Un des canvas est introuvable !");
        return;
    }

    new Chart(barCanvas, {
        type: "bar",
        data: {
            labels: ["Alger", "Oran", "Blida", "Boumerdes"],
            datasets: [{
                data: [6, 8, 1, 20],
                backgroundColor: ["red", "crimson", "lightgreen", "lightblue"]
            }]
        }
    });

    new Chart(pieCanvas, {
        type: "pie",
        data: {
            labels: ["Particulier", "Promoteur", "Agence Immobilière"],
            datasets: [{
                data: [20, 5, 8],
                backgroundColor: ["red", "lightgreen", "lightblue"]
            }]
        }
    });

    new Chart(lineCanvas, {
        type: "line",
        data: {
            labels: ["Janvier", "Février", "Mars", "Avril", "Mai", "Juin"],
            datasets: [{
                label: "Évolution des ventes",
                data: [3, 4, 20, 10, 15, 25], 
                borderColor: "blue",
                backgroundColor: "rgba(173, 216, 230, 0.5)", 
                fill: true
            }]
        }
    });


    new Chart(radarCanvas, {
        type: "radar",
        data: {
            labels: ["maison", "apartement", "duplex", "terrein", "garrage"],
            datasets: [{
                label: "bien vendu",
                data: [3, 4, 20, 10, 15], 
                borderColor: "red",
                backgroundColor: 'rgba(255, 99, 132, 0.2)', 
                fill: true
            }]
        }
    });

    new Chart(doughnutCanvas, {
        type: "doughnut",
        data: {
            labels: ["nombre inscription total", "nombre visite total", ],
            datasets: [{
                data: [18, 20],
                backgroundColor: ["#10B981", "#1310b9"]
            }]
        }
    });






});
