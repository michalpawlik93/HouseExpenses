import { createBrowserRouter } from 'react-router-dom'
import { HomeDetailsView } from '../views/HomeDetailsView'
import { ErrorBoundary } from './ErrorRouterBoundary'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <div> Tests</div>,
    errorElement: <ErrorBoundary />,
  },
  {
    path: 'homedetails',
    element: <HomeDetailsView />,
  },
])
