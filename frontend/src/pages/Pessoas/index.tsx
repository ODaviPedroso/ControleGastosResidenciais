import { useEffect, useState } from 'react';
import { pessoaService } from '../../services/pessoaService';
import type { Pessoa } from '../../types/Pessoa';

const empty = { nome: '', idade: '' };

export default function PessoasPage() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [form, setForm] = useState(empty);
  const [editId, setEditId] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  async function load() {
    setPessoas(await pessoaService.getAll());
  }

  useEffect(() => { load(); }, []);

  function startEdit(p: Pessoa) {
    setEditId(p.id);
    setForm({ nome: p.nome, idade: String(p.id) });
    setForm({ nome: p.nome, idade: String(p.idade) });
  }

  function cancelEdit() {
    setEditId(null);
    setForm(empty);
    setError('');
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setLoading(true);
    try {
      const data = { nome: form.nome, idade: Number(form.idade) };
      if (editId) {
        await pessoaService.atualizar(editId, data);
      } else {
        await pessoaService.criar(data);
      }
      setForm(empty);
      setEditId(null);
      await load();
    } catch {
      setError('Erro ao salvar pessoa.');
    } finally {
      setLoading(false);
    }
  }

  async function handleDelete(id: string, nome: string) {
    if (!confirm(`Deletar "${nome}"? Todas as transações serão removidas.`)) return;
    try {
      await pessoaService.deletar(id);
      await load();
    } catch {
      setError('Erro ao deletar pessoa.');
    }
  }

  return (
    <div>
      <h1>Pessoas</h1>

      <div className="form-card">
        <h2>{editId ? 'Editar Pessoa' : 'Nova Pessoa'}</h2>
        {error && <p className="msg-error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label>Nome</label>
              <input
                required
                maxLength={200}
                placeholder="Nome completo"
                value={form.nome}
                onChange={e => setForm(f => ({ ...f, nome: e.target.value }))}
              />
            </div>
            <div className="form-group" style={{ flex: '0 0 120px' }}>
              <label>Idade</label>
              <input
                required
                type="number"
                min={0}
                max={150}
                placeholder="0"
                value={form.idade}
                onChange={e => setForm(f => ({ ...f, idade: e.target.value }))}
              />
            </div>
            <div className="form-actions">
              <button className="btn btn-primary" type="submit" disabled={loading}>
                {editId ? 'Salvar' : 'Adicionar'}
              </button>
              {editId && (
                <button className="btn btn-secondary" type="button" onClick={cancelEdit}>
                  Cancelar
                </button>
              )}
            </div>
          </div>
        </form>
      </div>

      <div className="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Idade</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {pessoas.length === 0 ? (
              <tr><td colSpan={3} className="msg-empty">Nenhuma pessoa cadastrada.</td></tr>
            ) : pessoas.map(p => (
              <tr key={p.id}>
                <td>{p.nome}</td>
                <td>{p.idade} anos</td>
                <td>
                  <div className="td-actions">
                    <button className="btn btn-secondary btn-sm" onClick={() => startEdit(p)}>Editar</button>
                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(p.id, p.nome)}>Deletar</button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
