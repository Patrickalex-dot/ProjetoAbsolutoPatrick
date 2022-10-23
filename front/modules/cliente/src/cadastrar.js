//pegando os dados do formulário
async function CapturarDadosPessoa(){
     let nome = document.querySelector('#nomeCompleto').value;  
     console.log(nome);
     let cpf = document.querySelector('#cpf').value;  
     console.log(cpf);
     let dataNascimento = new Date(document.querySelector('#nascimento').value);  
     console.log(dataNascimento);
     let telefone = document.querySelector('#telefone').value;  
     console.log(telefone);
     let tipoContato = document.querySelector('#tipoContato').value;
     console.log(tipoContato);
     let rua = document.querySelector('#rua').value;  
     console.log(rua);
     let numero = document.querySelector('#numero').value;  
     console.log(numero);
     let cidade = document.querySelector('#cidade').value;  
     console.log(cidade);
     let bairro = document.querySelector('#bairro').value;  
     console.log(bairro);
     let referencia = document.querySelector('#complemento').value;  
     console.log(referencia);
     
     
     let cliente = {
       nome,
       cpf,
       dataNascimento,
       telefone,
       rua,
       numero,
       cidade,
       bairro,
       referencia,
       tipoContato
     };
     let salvarClienteViewModel = {
       cliente
     };
   
     console.log(salvarClienteViewModel);
   
     let response = await EnviarApi(salvarClienteViewModel);
     console.log(response);
   }
   
   //função para fazer uma request na api;
   async function EnviarApi(viewmodel){
     
     //opções/dados para fazer a request;
     const options = {
       //método, se é um post, get etc..
       method: 'POST',
       headers:{'content-type':'application/json'},
       //converte o objeto em um Json real;
       body:JSON.stringify(viewmodel) 
     };
   
     //TODO: mudar a url para o seu localhost.
     const req =  await fetch('https://localhost:44345/cliente/salvar2', options)
     //caso a request dê certo, retornará a resposta;
       .then(response => {
           return response.json();
       }) 
     //caso dê erro, irá retornar o erro e mostrar no console
       .catch(erro => {
           console.log(erro);
           return erro;
       });
       if(req.value.sucesso){
            alert(req.value.mensagem);
            voltar();
       }

       return req;
   }
function voltar(){
    window.location.href ='./listar.html';
}