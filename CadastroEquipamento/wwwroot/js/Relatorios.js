document.addEventListener("DOMContentLoaded", function () {
    const relatorioSelect = document.getElementById("relatorioSelect");
    const btnExportar = document.getElementById("btnExportar");

    btnExportar.addEventListener("click", function () {
        const tipo = relatorioSelect.value;

        if (!tipo) {
            alert("Selecione um relatório para exportar.");
            return;
        }

        // Cria formulário dinamicamente
        const form = document.createElement("form");
        form.method = "get"; // GET para download
        form.action = "/Relatorios/ExportarCsv";

        // Cria input hidden para o parâmetro tipo
        const input = document.createElement("input");
        input.type = "hidden";
        input.name = "tipo";
        input.value = tipo;
        form.appendChild(input);

        document.body.appendChild(form);
        form.submit();
    });
});