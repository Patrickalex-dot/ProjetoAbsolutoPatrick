async function PreencherTabelaCliente(resposta,limpar){
    let tabela = document.querySelector('#listagem-clientes');

    if(limpar)
    tabela.innerHTML = '';

    if(!resposta.value.sucesso)
        alert(resposta.mensage);
    else if(resposta.value.clientes.lenght == 0 ){
        tabela.innerHTML = 'Não há registro para exibir.';
    }
    else {
        resposta.value.clientes.forEach(function(e){
            let linha = document.createElement('tr');
            let btnsTd = document.createElement('td');
            let botaoEditar = document.createElement('input')
            botaoEditar.value ='Editar'
            botaoEditar.type = 'button';
            botaoEditar.addEventListener('click',()=>{
                    window.location.href ="atualizar.html?id=" + e.idCliente
                 });


            let botaoRemover = document.createElement('input');
            botaoRemover.value = 'Remover'
            botaoRemover.type = 'button';
            botaoRemover.addEventListener('click',async ()=>{
                     await remover(e.idCliente);
                });
            
            btnsTd.appendChild(botaoEditar);
            btnsTd.appendChild(botaoRemover);
            
            let idInput = document.createElement('input');
            idInput.type = 'hidden';
            let nomeTb = document.createElement('td');
            nomeTb.classList.add('row-nomeCompleto-cliente');
            let cpfTb = document.createElement('td');
            cpfTb.classList.add('row-cpf-cliente');
            let telefoneTb = document.createElement('td');
            telefoneTb.classList.add('row-telefone-cliente');
            
            idInput.value = e.idCliente;
            nomeTb.innerHTML = e.nome;
            cpfTb.innerHTML = e.cpf;
            telefoneTb.innerHTML = e.telefone;

            linha.appendChild(idInput);
            linha.appendChild(nomeTb);
            linha.appendChild(cpfTb);
            linha.appendChild(telefoneTb);
            
            linha.appendChild(btnsTd);
            
            tabela.appendChild(linha);
        })
    }
}

async function ListarClientes(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/cliente/BuscarTodos2',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}

async function remover (id){

    const options = {
        method : 'DELETE',
        Headers :{'content-type':'aplication/json'}
    };
    const req = await fetch('https://localhost:44345/cliente/Remover?id='+id, options)
    .then(response =>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    if(req.value.sucesso){
        alert(req.value.mensagem);
        voltar();
    }
    else {
        alert (req.mensagem);
    }
}
function voltar(){
    window.location.href ='../listar.html';
}
(async() =>{
    let res =await ListarClientes();
    PreencherTabelaCliente(res,false);
})();