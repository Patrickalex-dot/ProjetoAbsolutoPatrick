async function PreencherTabelaMotorista(resposta,limpar){
    let tabela = document.querySelector('#listagem-motoristas');

    if(limpar)
    tabela.innerHTML = '';

    if(!resposta.value.sucesso)
        alert(resposta.mensage);
    else if(resposta.value.resultado.lenght == 0 ){
        tabela.innerHTML = 'Não há registro para exibir.';
    }
    else {
        resposta.value.resultado.forEach(function(e){
            let linha = document.createElement('tr');
            let btnsTd = document.createElement('td');
            let botaoEditar = document.createElement('input')
            botaoEditar.value ='Editar'
            botaoEditar.type = 'button';
            botaoEditar.addEventListener('click',()=>{
                    window.location.href ="./atualizar.html?id=" + e.idMotorista
                 });


            let botaoRemover = document.createElement('input');
            botaoRemover.value = 'Remover'
            botaoRemover.type = 'button';
            botaoRemover.addEventListener('click',async ()=>{
                     await remover(e.idMotorista);
                });
            
            btnsTd.appendChild(botaoEditar);
            btnsTd.appendChild(botaoRemover);
            
            let idInputMotorista = document.createElement('input');
            idInputMotorista.type = 'hidden';
            idInputMotorista.id = "id-motorista";    
            let nomeTb = document.createElement('td');
            nomeTb.classList.add('row-nome-motorista');
            let cnhTb = document.createElement('td');
            cnhTb.classList.add('row-cnh-motorista');
            let telefoneTb = document.createElement('td');
            telefoneTb.classList.add('row-telefone-motorista');            
            let idInputVeiculo = document.createElement('input');
            idInputVeiculo.type = 'hidden';
            idInputVeiculo.id = "id-veiculo";
            let placaTb = document.createElement('td');
            placaTb.classList.add('row-placa-veiculo');

            idInputMotorista.value = e.idMotorista;
            if(e.veiculo){
                idInputVeiculo.value = e.veiculo.idVeiculo;
                placaTb.innerHTML = e.veiculo.placa;
            }
            nomeTb.innerHTML = e.nome;
            cnhTb.innerHTML = e.cnh;
            telefoneTb.innerHTML = e.telefone;
            
        

            linha.appendChild(idInputMotorista);
            linha.appendChild(idInputVeiculo);
            linha.appendChild(nomeTb);
            linha.appendChild(cnhTb);
            linha.appendChild(telefoneTb);
            linha.appendChild(placaTb);
            
            linha.appendChild(btnsTd);
            
            tabela.appendChild(linha);
        })
    }
}

async function ListarMotoristas(){
    const options = {
        method: 'GET',
        headers:{'content-type': 'application/json'}
    };
    const req = await fetch ('https://localhost:44345/motorista/BuscarTodos',options)
    .then(response=>{
        return response.json();
    })
    .catch(erro =>{
        console.log(erro);
        return erro;
    });
    return req;
}

async function remover(id){

    const options = {
        method : 'DELETE',
        headers :{'content-type':'application/json'}
    };
    const req = await fetch('https://localhost:44345/motorista/Remover?id='+id, options)
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
    window.location.href ='./listar.html';
}
(async() =>{
    let res =await ListarMotoristas();
    PreencherTabelaMotorista(res,false);
})();