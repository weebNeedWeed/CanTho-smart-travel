package templates

import _ "embed"

//go:embed instruction.tmpl
var RawInstruction string

//go:embed default.tmpl
var RawPrompt string
