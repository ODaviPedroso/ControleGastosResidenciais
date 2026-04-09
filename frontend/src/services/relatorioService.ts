import type { TotaisResponse } from '../types/Relatorio';
import api from './api';

export const relatorioService = {
  getTotaisPorPessoa: () => api.get<TotaisResponse>('/api/relatorios/totais-por-pessoa').then(r => r.data),
  getTotaisPorCategoria: () => api.get<TotaisResponse>('/api/relatorios/totais-por-categoria').then(r => r.data),
};
