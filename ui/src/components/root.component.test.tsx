import { render, screen } from '@testing-library/react'
import Root from './root.component'

test('renders learn react link', () => {
  render(<Root />)
  const linkElement = screen.getByText(/learn react/i)
  expect(linkElement).toBeDefined()
})
