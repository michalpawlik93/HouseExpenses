import React from 'react'
import { StyledEngineProvider, ThemeProvider } from '@mui/material'
import { createTheme } from './assets/themes/createTheme'

const App: React.FC = () => {
  const theme = createTheme()
  return (
    <StyledEngineProvider injectFirst>
      <ThemeProvider theme={theme}>
        <>Tests</>{' '}
      </ThemeProvider>
    </StyledEngineProvider>
  )
}

export default App
