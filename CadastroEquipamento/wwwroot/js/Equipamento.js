const showMessage = (modal, msg, isSuccess = true) => {
    const alertType = isSuccess ? "alert-success" : "alert-danger";
    const alertDiv = `
        <div class="alert ${alertType} alert-dismissible fade show" role="alert">
            ${msg}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;
    modal.find(".alert-placeholder").html(alertDiv);
};

$(document).ready(function () {


    $(".createEquipamentoForm").submit(function (e) {
        e.preventDefault();
        const form = $(this);
        const modal = form.closest(".modal");
        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);
        obj.Status = obj["Status-create"] === "true";


        if (obj.DataAquisicao && new Date(obj.DataAquisicao) > new Date()) {
            showMessage(modal, "A data de aquisição não pode ser maior que a data atual.", false);
            return;
        }

        $.ajax({
            url: "/Equipamento/Create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                showMessage(modal, result.message, result.success);
                if (result.success) setTimeout(() => location.reload(), 1500);
            },
            error: function (err) {
                showMessage(modal, "Erro na requisição: " + err.responseText, false);
            }
        });
    });


    $(".editEquipamentoForm").submit(function (e) {
        e.preventDefault();
        const form = $(this);
        const modal = form.closest(".modal");
        const id = form.data("id");
        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);
        obj.Status = obj[`Status-${id}`] === "true";


        if (obj.DataAquisicao && new Date(obj.DataAquisicao) > new Date()) {
            showMessage(modal, "A data de aquisição não pode ser maior que a data atual.", false);
            return;
        }

        $.ajax({
            url: "/Equipamento/Edit",
            type: "PATCH",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                showMessage(modal, result.message, result.success);
                if (result.success) setTimeout(() => location.reload(), 1500);
            },
            error: function (err) {
                showMessage(modal, "Erro na requisição: " + err.responseText, false);
            }
        });
    });
});

function DeletarEquipamento(btn) {
    const id = $(btn).data("id");
    if (!confirm("Deseja realmente excluir este equipamento?")) return;

    $.ajax({
        url: "/Equipamento/Delete",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(id),
        success: function (result) {
            alert(result.message);
            if (result.success) location.reload();
        },
        error: function (err) {
            alert("Erro na requisição: " + err.responseText);
        }
    });
}
