document.addEventListener("DOMContentLoaded", function () {
    var vincularModal = document.getElementById('vincularModal');

    // Preenche modal com EquipamentoId e seleciona usuário atual
    vincularModal.addEventListener('show.bs.modal', function (event) {
        var button = event.relatedTarget;
        var equipamentoId = button.getAttribute('data-equipamentoid');
        var usuarioId = button.getAttribute('data-usuarioid');

        document.getElementById('EquipamentoId').value = equipamentoId;
        document.getElementById('UsuarioSelect').value = usuarioId || "";
    });

    // Submit do formulário usando VinculoViewModel
    var form = document.getElementById('formVinculo');
    form.addEventListener('submit', function (e) {
        e.preventDefault();

        var data = {
            EquipamentoId: document.getElementById('EquipamentoId').value,
            UsuarioId: document.getElementById('UsuarioSelect').value
        };

        fetch('/Vinculo/Vincular', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }).then(() => location.reload());
    });

    // Desvincular
    document.querySelectorAll('.desvincular-btn').forEach(function (btn) {
        btn.addEventListener('click', function () {
            var data = { EquipamentoId: this.getAttribute('data-equipamentoid') };
            fetch('/Vinculo/Desvincular', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }).then(() => location.reload());
        });
    });
});