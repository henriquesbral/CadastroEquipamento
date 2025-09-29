document.addEventListener("DOMContentLoaded", function () {
    const equipamentoSelect = document.getElementById('EquipamentoSelect');
    const usuarioSelect = document.getElementById('UsuarioSelect');
    const btnVincular = document.getElementById('btnVincular');
    const form = document.getElementById('formVinculo');
    const msgVinculo = document.getElementById('msgVinculo');

    equipamentoSelect?.addEventListener('change', function () {
        const equipamentoId = this.value;

        if (!equipamentoId) {
            usuarioSelect.innerHTML = '<option value="">-- Selecione --</option>';
            usuarioSelect.disabled = true;
            btnVincular.disabled = true;
            return;
        }

        fetch(`/Vinculo/UsuariosDisponiveis?equipamentoId=${equipamentoId}`)
            .then(res => res.json())
            .then(result => {
                usuarioSelect.innerHTML = '<option value="">-- Selecione --</option>';

                if (result.success && Array.isArray(result.data)) {
                    result.data.forEach(u => {
                        const opt = document.createElement('option');
                        opt.value = u.codUsuario;
                        opt.textContent = u.nome;
                        usuarioSelect.appendChild(opt);
                    });
                    usuarioSelect.disabled = false;
                    btnVincular.disabled = false;
                } else {
                    console.error("Erro ao carregar usuários:", result.message || result);
                    usuarioSelect.disabled = true;
                    btnVincular.disabled = true;
                }
            })
            .catch(err => console.error(err));
    });

    
    form.addEventListener('submit', function (e) {
        e.preventDefault();

        const data = {
            CodEquipamento: equipamentoSelect.value,
            CodUsuario: usuarioSelect.value
        };

        fetch('/Vinculo/Vincular', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        })
            .then(res => res.json())
            .then(result => {
                msgVinculo.classList.remove("d-none", "alert-success", "alert-danger");
                msgVinculo.classList.add(result.success ? "alert-success" : "alert-danger");
                msgVinculo.textContent = result.message;

                if (result.success) {
                    setTimeout(() => location.reload(), 1200);
                }
            })
            .catch(err => console.error(err));
    });

});

function Desvincular(btn) {
    const equipamentoId = $(btn).data("equipamentoid");
    const usuarioId = $(btn).data("usuarioid");
    const vinculoId = $(btn).data("vinculoid");

    if (!confirm("Deseja realmente excluir este vínculo?")) return;

    $.ajax({
        url: "/Vinculo/Desvincular",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            EquipamentoId: equipamentoId,
            UsuarioId: usuarioId,
            VinculoId: vinculoId
        }),
        success: function (result) {
            alert(result.message);
            if (result.success) location.reload();
        },
        error: function (err) {
            alert("Erro na requisição: " + err.responseText);
        }
    });
}