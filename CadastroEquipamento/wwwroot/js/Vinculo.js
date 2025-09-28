document.addEventListener("DOMContentLoaded", function () {
    const equipamentoSelect = document.getElementById('EquipamentoSelect');
    const usuarioSelect = document.getElementById('UsuarioSelect');
    const equipamentoIdInput = document.getElementById('EquipamentoId');
    const btnVincular = document.getElementById('btnVincular');
    const form = document.getElementById('formVinculo');

    // Atualiza usuários disponíveis ao selecionar equipamento
    equipamentoSelect?.addEventListener('change', function () {
        const equipamentoId = this.value;
        equipamentoIdInput.value = equipamentoId;

        if (!equipamentoId) {
            usuarioSelect.innerHTML = '<option value="">-- Selecione --</option>';
            usuarioSelect.disabled = true;
            btnVincular.disabled = true;
            return;
        }

        fetch(`/Vinculo/UsuariosDisponiveis?equipamentoId=${equipamentoId}`)
            .then(res => res.json())
            .then(data => {
                usuarioSelect.innerHTML = '<option value="">-- Selecione --</option>';
                data.forEach(u => {
                    const opt = document.createElement('option');
                    opt.value = u.codUsuario;
                    opt.textContent = u.nome;
                    usuarioSelect.appendChild(opt);
                });
                usuarioSelect.disabled = false;
                btnVincular.disabled = false;
            })
            .catch(err => console.error(err));
    });

    // Preenche modal ao abrir
    const vincularModal = document.getElementById('vincularModal');
    vincularModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const equipamentoId = button.getAttribute('data-equipamentoid');
        const usuarioId = button.getAttribute('data-usuarioid');

        equipamentoSelect.value = equipamentoId || "";
        equipamentoIdInput.value = equipamentoId || "";
        usuarioSelect.value = usuarioId || "";
        btnVincular.disabled = !equipamentoId;
    });

    // Submit do vínculo
    form.addEventListener('submit', function (e) {
        e.preventDefault();

        const data = {
            EquipamentoId: equipamentoIdInput.value,
            UsuarioId: usuarioSelect.value
        };

        fetch('/Vinculo/Vincular', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        })
            .then(res => {
                if (res.ok) location.reload();
                else alert("Erro ao vincular equipamento.");
            })
            .catch(err => console.error(err));
    });

    // Desvincular
    document.querySelectorAll('.desvincular-btn').forEach(btn => {
        btn.addEventListener('click', function () {
            const equipamentoId = this.getAttribute('data-equipamentoid');
            fetch('/Vinculo/Desvincular', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ EquipamentoId: equipamentoId })
            })
                .then(res => {
                    if (res.ok) location.reload();
                    else alert("Erro ao desvincular equipamento.");
                })
                .catch(err => console.error(err));
        });
    });
});
