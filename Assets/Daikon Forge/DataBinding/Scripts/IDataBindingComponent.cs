﻿// @cond DOXY_IGNORE
/* Copyright 2013 Daikon Forge */

using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is a marker interface used by the Editor components to 
/// identify Component types that can be displayed in a context menu.
/// </summary>
public interface IDataBindingComponent
{
	void Bind();
	void Unbind();
}

// @endcond DOXY_IGNORE
