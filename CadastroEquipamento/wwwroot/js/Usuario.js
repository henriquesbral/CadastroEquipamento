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

$(document).ready(function () {

    $(".edit-btn").click(function () {
        const btn = $(this);
        const modal = $("#editUsuarioModal");

        modal.find("input[name=CodUsuario]").val(btn.data("id"));
        modal.find("input[name=Nome]").val(btn.data("nome"));
        modal.find("input[name=Email]").val(btn.data("email"));
        modal.find("input[name=Departamento]").val(btn.data("departamento"));
    });

    $(".createUsuarioForm").submit(function (e) {
        e.preventDefault();

        const nome = $("#nome").val().trim();
        const email = $("#email").val().trim();
        const departamento = $("#departamento").val().trim();

        // Validação simples
        if (!nome) {
            alert("O campo Nome é obrigatório!");
            return;
        }
        if (!email) {
            alert("O campo Email é obrigatório!");
            return;
        }
        if (!departamento) {
            alert("O campo Departamento é obrigatório!");
            return;
        }

        const usuario = {
            Nome: nome,
            Email: email,
            Departamento: departamento
        };

        $.ajax({
            url: "/Usuario/Create",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(usuario),
            success: function (result) {
                showMessage(result.message, result.success, () => {
                    if (result.success) {
                        location.reload();
                    }
                });
            },
            error: function (err) {
                showMessage("Erro ao cadastrar usuário: " + err.responseText, false);
            }
        });
    });

    $(".editUsuarioForm").submit(function (e) {
        if (!confirm("Deseja realmente alterar os dados deste usuario?")) return;
        e.preventDefault();
        const form = $(this);
        const modal = form.closest(".modal");
        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);

        $.ajax({
            url: "/Usuario/Edit",
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

});

function DeletarUsuario(btn) {
    const id = $(btn).data("id");
    console.log("O id é: " + id)
    if (!confirm("Deseja realmente excluir este usuario?")) return;

    $.ajax({
        url: "/Usuario/Delete",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(id),
        success: function (result) {
            showMessage(result.message, result.success, () => {
                if (result.success) location.reload();
            });
        },
        error: function (err) {
            showMessage("Erro na requisição: " + err.responseText, false);
        }
    });
}

