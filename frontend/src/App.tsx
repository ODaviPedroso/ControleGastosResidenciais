import { BrowserRouter, Navigate, NavLink, Route, Routes } from 'react-router-dom';
import CategoriasPage from './pages/Categorias';
import PessoasPage from './pages/Pessoas';
import RelatoriosPage from './pages/Relatorios';
import TransacoesPage from './pages/Transacoes';
import './App.css';

export default function App() {
  return (
    <BrowserRouter>
      <nav className="navbar">
        <NavLink to="/pessoas">Pessoas</NavLink>
        <NavLink to="/categorias">Categorias</NavLink>
        <NavLink to="/transacoes">Transações</NavLink>
        <NavLink to="/relatorios">Relatórios</NavLink>
      </nav>

      <main className="content">
        <Routes>
          <Route path="/" element={<Navigate to="/pessoas" replace />} />
          <Route path="/pessoas" element={<PessoasPage />} />
          <Route path="/categorias" element={<CategoriasPage />} />
          <Route path="/transacoes" element={<TransacoesPage />} />
          <Route path="/relatorios" element={<RelatoriosPage />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}
