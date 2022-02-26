var tbody = document.querySelector('table tbody');
var aluno = {};

function cadastrar() {
    
    aluno.Nome = document.querySelector('#nome').value;
    aluno.Sobrenome = document.querySelector('#sobrenome').value;
    aluno.Telefone = document.querySelector('#telefone').value;
    aluno.Ra = document.querySelector('#ra').value;
    
    console.log(aluno);
    
    if(aluno.Id === 0 || aluno.Id === undefined){
        salvarEstudantes('POST',0, aluno);
    }
    else{
        salvarEstudantes('PUT',aluno.Id, aluno);
    }
    
    carregaEstudantes();
    
    $('#myModal').modal('hide');
}

function cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';
    
    aluno = {};
    
    btnSalvar.textContent = 'Cadastrar';
    tituloModal.textContent = 'Cadastrar Aluno';
    
    $('#myModal').modal('hide');
}

function novoAluno(){
    var btnSalvar = document.querySelector('#btnSalvar');
    var tituloModal = document.querySelector('#tituloModal');
    document.querySelector('#nome').value = '';
    document.querySelector('#sobrenome').value = '';
    document.querySelector('#telefone').value = '';
    document.querySelector('#ra').value = '';
    
    aluno = {};
    
    btnSalvar.textContent = 'Cadastrar';
    tituloModal.textContent = 'Cadastrar Aluno';
    
    $('#myModal').modal('show');
}

function carregaEstudantes() {
    tbody.innerHTML = '';
    
    var xhr = new XMLHttpRequest();
    
    xhr.open(`GET`,`https://localhost:44328/api/aluno/Recuperar `, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));
    
    xhr.onerror = function(){
        console.log('ERRO', xhr.readyState)
    }
    
    xhr.onreadystatechange = function () {
        if(this.readyState == 4){ 
            if(this.status == 200){
                var estudantes = JSON.parse(this.responseText);
                console.log('DONE', xhr.readyState)
                for(var indice in estudantes){
                    adicionaLinha(estudantes[indice]); 
                }
            }else if(this.status == 500){
                var erro = JSON.parse(this.responseText);
                console.log(erro.Message);
                console.log(erro.ExceptionMessage);
            }
        }
    }
    xhr.send();
}

function salvarEstudantes(metodo, id, corpo) {
    var xhr = new XMLHttpRequest();
    
    if(id === undefined || id === 0)
    id = '';
    
    xhr.open(metodo,`https://localhost:44328/api/aluno/${id}`, false);
    
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(corpo));
}

function excluirEstudante(id) {
    var xhr = new XMLHttpRequest();
    
    
    xhr.open(`DELETE`,`https://localhost:44328/api/aluno/${id}`, false);
    
    xhr.send();
}

function excluir (estudante) {
    
    bootbox.confirm({
        message: `Tem certeza que deseja excluir o estudante portador do RA ${estudante.Ra}?`,
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if(result){
                excluirEstudante(estudante.Id);
                carregaEstudantes();
            }
        }
    });
}


carregaEstudantes();

function editarEstudante(estudante) {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var tituloModal = document.querySelector('#tituloModal');
    document.querySelector('#nome').value = estudante.Nome;
    document.querySelector('#sobrenome').value = estudante.Sobrenome;
    document.querySelector('#telefone').value = estudante.Telefone;
    document.querySelector('#ra').value = estudante.Ra;
    
    btnSalvar.textContent = 'Salvar';
    
    tituloModal.textContent = `Editar Aluno ${estudante.Nome}`;
    aluno = estudante;
    
    console.log(aluno);
    
}



function adicionaLinha(estudante) {
    
    var trow = `<tr>
    <td>${estudante.nome}</td>
    <td>${estudante.sobrenome}</td>
    <td>${estudante.telefone}</td>
    <td>${estudante.ra}</td>
    <td><button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='editarEstudante(${JSON.stringify(estudante)})'>Editar</button></td>
    <td><button class="btn btn-danger" onclick='excluir(${JSON.stringify(estudante)})'>Excluir</button></td>
    <tr>
    `
    tbody.innerHTML += trow;
}
