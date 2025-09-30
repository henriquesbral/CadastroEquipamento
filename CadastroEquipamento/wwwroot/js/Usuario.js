const showMessage = (msg, isSuccess = true, callback) => {
    const alertType = isSuccess ? "alert-success" : "alert-danger";
    const alertDiv = $(`
        <div class="alert ${alertType} alert-dismissible fade show" role="alert">
            ${msg}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `);

    const openModal = $('.modal.show');

    if (openModal.length > 0) {
        openModal.find('.modal-body').prepend(alertDiv);
    } else {
        $("body").prepend(alertDiv);
    }

    setTimeout(() => {
        alertDiv.alert('close'); 
        if (callback) callback();
    }, 3000); 
};

$(document).ready(function () {
    $('.modal').on('hidden.bs.modal', function () {
        $(this).find('.alert').remove();
    });

    $('#createUsuarioModal').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
        $(this).find('.alert').remove();
    });

    $(".edit-btn").click(function () {
        const btn = $(this);
        const modal = $("#editUsuarioModal");

        modal.find('.alert').remove();

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

        $(this).closest('.modal').find('.alert').remove();

        if (!nome) {
            showMessage("O campo Nome é obrigatório!", false);
            $("#nome").focus();
            return;
        }
        if (!email) {
            showMessage("O campo Email é obrigatório!", false);
            $("#email").focus();
            return;
        }
        if (!departamento) {
            showMessage("O campo Departamento é obrigatório!", false);
            $("#departamento").focus();
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
                        const modal = bootstrap.Modal.getInstance($('#createUsuarioModal')[0]);
                        if (modal) modal.hide();
                        location.reload();
                    }
                });
            },
            error: function (err) {
                showMessage("Erro ao cadastrar usuário: " + (err.responseJSON?.message || err.responseText), false);
            }
        });
    });

    $(".editUsuarioForm").submit(function (e) {
        e.preventDefault();

        const form = $(this);
        const modal = form.closest(".modal");

        modal.find('.alert').remove();

        if (!confirm("Deseja realmente alterar os dados deste usuário?")) return;

        const data = form.serializeArray();
        const obj = {};
        data.forEach(item => obj[item.name] = item.value);

        $.ajax({
            url: "/Usuario/Edit",
            type: "PATCH",
            contentType: "application/json",
            data: JSON.stringify(obj),
            success: function (result) {
                showMessage(result.message, result.success, () => {
                    if (result.success) {
                        const bootstrapModal = bootstrap.Modal.getInstance(modal[0]);
                        if (bootstrapModal) bootstrapModal.hide();
                        location.reload();
                    }
                });
            },
            error: function (err) {
                showMessage("Erro na requisição: " + (err.responseJSON?.message || err.responseText), false);
            }
        });
    });
});

function DeletarUsuario(btn) {
    const id = $(btn).data("id");

    if (!confirm("Deseja realmente excluir este usuário?")) return;

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
            showMessage("Erro na requisição: " + (err.responseJSON?.message || err.responseText), false);
        }
    });
}
