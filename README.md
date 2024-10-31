# IaFuture

API do projeto IaFuture - Software que controla resultados de análises feitas por IA para empresas que fornecem convênios médicos

# Representantes 

- Gabriel Ortiz Oliva Gil - RM: 98642 – 2TDSPK
- Rafael Noboru Watanabe Nasaha - RM:99948 – 2TDSPK
- João Pedro Kraide Máximo - RM:550974 – 2TDSPK
- Matheus de Andrade Ferreira - RM:99375 – 2TDSPK
- Larissa Pereira Biusse - RM:551062 - 2TDSPK

# Objetivos da aplicação

- API voltada para uma empresa de convênios médicos com possibilidade de cadastrar seus clientes e planos de saúde disponíveis
- Com os clientes e os planos cadastrados, é possivel recolher dados e oferecer através de um Machine Learning do .NET, o melhor plano de saúde para o cliente
- O Objetivo principal é fazer com que a empresa tome as melhores escolhas com seus clientes e consequentemente obter mais crescimento

# Explicação do modelo da aplicação

Foi adotado a abordagem monolítica para a aplicação pelos seguintes motivos:

- Por se tratar de um projeto menor e sem muitas complexidades, uma abordagem monolítica permite um desenvolvimento mais rápido
- Simplicidade na arquitetura, manter tudo em um único projeto facilita a compreensão e manutenção inicial por estar tudo em um único código base
- A integração entre componentes é mais direta e isso facilita a implementação de testes
- A latência é reduzida como também a sobrecarga associada à comunicação entre serviços distribuídos 
- Mais fácil de gerenciar transições que envolvem múltiplos componentes 

# Recursos disponíveis na aplicação

- CRUD com banco de dados Oracle e também possibilidade de conexão com o MongoDB
- Login com autenticação e criptografia de senhas
- Implementação do serviço externo Stripe para pagamentos
- Implementação do ML.NET para análise dos dados e previsão
- Possibilidade de verificar a saúde da aplicação através do HealthCheck
- Clean Code aplicado separando devidamente as bibliotecas de classes e implementando extensões 

# Como rodar a aplicação

- Clonar o projeto do repositório no GitHub
- Lembrar de alterar as informações do banco de dados no "appsettings.json" de acordo com seus dados
- Executar o projeto no VS Code ou outra IDE para linguagem C# e projeto API Web Core
- Caso queira verificar a saúde da aplicação, acesse com https://localhost:{port}/health
- Ao executar, uma página web do Swagger irá abrir
- No Swagger, teremos o CRUD de todas as classes
- Necessário antes de utilizar os endpoints de /api/Clientes, em "Authorize" colocar seu Token para estar autenticado e fazer o CRUD
- Para testar os endpoints, basta clicar em "try it out" e preencher de acordo com cada um
- Utilizar os seguintes JSON de exemplo nos métodos POST:

-> POST de Clientes

{
    "nome": "João da Silva",
    "usuario": "joao.silva",
    "idade": 30,
    "sexo": "Masculino",
    "profissao": "Engenheiro",
    "estadoSaude": "Saudável",
    "senhaHash": "SenhaSegura123"
}

-> POST de Planos

{
    "nome": "Plano Saúde Premium",
    "preco": 299.99,
    "tipoPlano": "Basico",
    "cobertura": "Cobertura completa para consultas, exames e internações.",
    "publicoAlvo": "Individuais" }

	
