$(document).ready(function () {

    const showMessage = (msg, isSuccess = true, callback) => {
        const alertType = isSuccess ? "alert-success" : "alert-danger";
        const alertDiv = $(`
            <div class="alert ${alertType} alert-dismissible fade show" role="alert">
                ${msg}
                <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
            </div>
        `);
        $("body").prepend(alertDiv);

        setTimeout(() => {
            alertDiv.remove();
            if (callback) callback();
        }, 1000);
    };

    // Criar equipamento
    $(".createEquipamentoForm").submit(function (e) {
        e.preventDefault();
        const form = $(this);
        const modal = form.closest(".modal"); // captura a modal atual
        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);
        obj.Status = obj["Status-create"] === "true";

        $.ajax({
            url: "/Equipamento/Create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                const bootstrapModal = bootstrap.Modal.getInstance(modal[0]);
                if (bootstrapModal) bootstrapModal.hide();

                showMessage(result.message, result.success, () => {
                    if (result.success) location.reload();
                });
            },
            error: function (err) {
                showMessage("Erro na requisição: " + err.responseText, false);
            }
        });
    });

    // Editar equipamento
    $(".editEquipamentoForm").submit(function (e) {
        e.preventDefault();
        const form = $(this);
        const modal = form.closest(".modal");
        const id = form.data("id");
        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);
        obj.Status = obj[`Status-${id}`] === "true";

        $.ajax({
            url: "/Equipamento/Edit",
            type: "PATCH",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                const bootstrapModal = bootstrap.Modal.getInstance(modal[0]);
                if (bootstrapModal) bootstrapModal.hide();

                showMessage(result.message, result.success, () => {
                    if (result.success) location.reload();
                });
            },
            error: function (err) {
                showMessage("Erro na requisição: " + err.responseText, false);
            }
        });
    });

    // Excluir equipamento
    $(".delete-btn").click(function () {
        if (!confirm("Deseja realmente excluir este equipamento?")) return;
        const id = $(this).val();

        $.ajax({
            url: "/Equipamento/Delete",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ codEquipamento: id }),
            success: function (result) {
                showMessage(result.message, result.success, () => {
                    if (result.success) location.reload();
                });
            },
            error: function (err) {
                showMessage("Erro na requisição: " + err.responseText, false);
            }
        });
    });

});