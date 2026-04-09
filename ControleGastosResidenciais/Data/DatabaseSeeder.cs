using FinanceiroApi.Models;

namespace ControleGastosResidenciais.Data
{
    // Popula o banco com dados de exemplo ao iniciar a aplicação em ambiente de desenvolvimento só insere dados se o banco estiver vazio, evitando dados repetidos.
    // Aqui fui pela logica de criar uma 'famila' com mãe pai e filho, assim acabo cubrindo todos os cenarios da aplicação
    public static class DatabaseSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Pessoas.Any())
                return;

            //Pessoas
            var joao   = new Pessoa { Nome = "João Silva",    Idade = 35 };
            var maria  = new Pessoa { Nome = "Maria Santos",  Idade = 28 };
            var pedro  = new Pessoa { Nome = "Pedro Alves",   Idade = 16 };

            context.Pessoas.AddRange(joao, maria, pedro);

            //Categorias 
            var alimentacao = new Categoria { Descricao = "Alimentação",  Finalidade = Finalidade.Despesa };
            var salario     = new Categoria { Descricao = "Salário",      Finalidade = Finalidade.Receita };
            var transporte  = new Categoria { Descricao = "Transporte",   Finalidade = Finalidade.Despesa };
            var freelance   = new Categoria { Descricao = "Freelance",    Finalidade = Finalidade.Receita };
            var lazer       = new Categoria { Descricao = "Lazer",        Finalidade = Finalidade.Ambas  };

            context.Categorias.AddRange(alimentacao, salario, transporte, freelance, lazer);

            //Transações — João (35 anos pai) 
            context.Transacoes.AddRange(
                new Transacao { Descricao = "Salário mensal",       Valor = 5000.00m, Tipo = TipoTransacao.Receita,  Categoria = salario,    Pessoa = joao  },
                new Transacao { Descricao = "Supermercado",         Valor =  650.00m, Tipo = TipoTransacao.Despesa,  Categoria = alimentacao, Pessoa = joao  },
                new Transacao { Descricao = "Passagem de ônibus",   Valor =  180.00m, Tipo = TipoTransacao.Despesa,  Categoria = transporte,  Pessoa = joao  },
                new Transacao { Descricao = "Consultoria pontual",  Valor = 1200.00m, Tipo = TipoTransacao.Receita,  Categoria = freelance,   Pessoa = joao  },
                new Transacao { Descricao = "Cinema e jantar",      Valor =  220.00m, Tipo = TipoTransacao.Despesa,  Categoria = lazer,       Pessoa = joao  }
            );

            //Transações — Maria (28 anos mãe)
            context.Transacoes.AddRange(
                new Transacao { Descricao = "Salário mensal",       Valor = 7500.00m, Tipo = TipoTransacao.Receita,  Categoria = salario,    Pessoa = maria },
                new Transacao { Descricao = "Feira semanal",        Valor =  300.00m, Tipo = TipoTransacao.Despesa,  Categoria = alimentacao, Pessoa = maria },
                new Transacao { Descricao = "Uber mensal",          Valor =  250.00m, Tipo = TipoTransacao.Despesa,  Categoria = transporte,  Pessoa = maria },
                new Transacao { Descricao = "Projeto freelance",    Valor = 2000.00m, Tipo = TipoTransacao.Receita,  Categoria = freelance,   Pessoa = maria },
                new Transacao { Descricao = "Show de música",       Valor =  150.00m, Tipo = TipoTransacao.Despesa,  Categoria = lazer,       Pessoa = maria }
            );

            //Transações — Pedro (16 anos filho: é para ser o cara que tem apenas despesas)
            context.Transacoes.AddRange(
                new Transacao { Descricao = "Lanche na escola",     Valor =   80.00m, Tipo = TipoTransacao.Despesa,  Categoria = alimentacao, Pessoa = pedro },
                new Transacao { Descricao = "Passagem escolar",     Valor =   60.00m, Tipo = TipoTransacao.Despesa,  Categoria = transporte,  Pessoa = pedro },
                new Transacao { Descricao = "Videogame",            Valor =  200.00m, Tipo = TipoTransacao.Despesa,  Categoria = lazer,       Pessoa = pedro }
            );

            context.SaveChanges();
        }
    }
}
