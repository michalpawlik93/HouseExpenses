import React from 'react'
import ReactDOM from 'react-dom/client'
import './assets/index.css'
import Root from './components/root.component'

const rootElement = document.getElementById('root') as Element
const root = ReactDOM.createRoot(rootElement)

root.render(
  <React.StrictMode>
    <Root />
  </React.StrictMode>
)
