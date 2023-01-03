import { createBrowserRouter } from 'react-router-dom'
import { HomeDetailsView } from '../views/HomeDetailsView'
import { ErrorBoundary } from './ErrorRouterBoundary'
import { Layout } from '../components/navigation/Layout'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    errorElement: <ErrorBoundary />,
    children: [
      {
        path: 'homedetails',
        element: <HomeDetailsView />,
      },
    ],
  },
])
