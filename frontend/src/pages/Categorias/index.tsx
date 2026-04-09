import { useEffect, useState } from 'react';
import { categoriaService } from '../../services/categoriaService';
import type { Categoria } from '../../types/Categoria';
import { Finalidade } from '../../types/Categoria';

const finalidadeLabel: Record<number, string> = {
  [Finalidade.Despesa]: 'Despesa',
  [Finalidade.Receita]: 'Receita',
  [Finalidade.Ambas]: 'Ambas',
};

const finalidadeBadge: Record<number, string> = {
  [Finalidade.Despesa]: 'badge badge-despesa',
  [Finalidade.Receita]: 'badge badge-receita',
  [Finalidade.Ambas]: 'badge badge-ambas',
};

const empty = { descricao: '', finalidade: String(Finalidade.Despesa) };

export default function CategoriasPage() {
  const [categorias, setCategorias] = useState<Categoria[]>([]);
  const [form, setForm] = useState(empty);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  async function load() {
    setCategorias(await categoriaService.getAll());
  }

  useEffect(() => { load(); }, []);

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError('');
    setLoading(true);
    try {
      await categoriaService.criar({
        descricao: form.descricao,
        finalidade: Number(form.finalidade) as Finalidade,
      });
      setForm(empty);
      await load();
    } catch {
      setError('Erro ao criar categoria.');
    } finally {
      setLoading(false);
    }
  }

  return (
    <div>
      <h1>Categorias</h1>

      <div className="form-card">
        <h2>Nova Categoria</h2>
        {error && <p className="msg-error">{error}</p>}
        <form onSubmit={handleSubmit}>
          <div className="form-row">
            <div className="form-group">
              <label>Descrição</label>
              <input
                required
                maxLength={400}
                placeholder="Ex: Alimentação"
                value={form.descricao}
                onChange={e => setForm(f => ({ ...f, descricao: e.target.value }))}
              />
            </div>
            <div className="form-group" style={{ flex: '0 0 160px' }}>
              <label>Finalidade</label>
              <select
                value={form.finalidade}
                onChange={e => setForm(f => ({ ...f, finalidade: e.target.value }))}
              >
                <option value={Finalidade.Despesa}>Despesa</option>
                <option value={Finalidade.Receita}>Receita</option>
                <option value={Finalidade.Ambas}>Ambas</option>
              </select>
            </div>
            <div className="form-actions">
              <button className="btn btn-primary" type="submit" disabled={loading}>
                Adicionar
              </button>
            </div>
          </div>
        </form>
      </div>

      <div className="table-wrapper">
        <table>
          <thead>
            <tr>
              <th>Descrição</th>
              <th>Finalidade</th>
            </tr>
          </thead>
          <tbody>
            {categorias.length === 0 ? (
              <tr><td colSpan={2} className="msg-empty">Nenhuma categoria cadastrada.</td></tr>
            ) : categorias.map(c => (
              <tr key={c.id}>
                <td>{c.descricao}</td>
                <td>
                  <span className={finalidadeBadge[c.finalidade]}>
                    {finalidadeLabel[c.finalidade]}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
