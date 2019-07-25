﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace FASTER.core
{
    /// <summary>
    /// Interface for users to control creation and retrieval of checkpoint-related data
    /// </summary>
    public interface ICheckpointManager
    {
        /// <summary>
        /// Initialize index checkpoint
        /// </summary>
        /// <param name="indexToken"></param>
        /// <returns></returns>
        void InitializeIndexCheckpoint(Guid indexToken);

        /// <summary>
        /// Initialize log checkpoint
        /// </summary>
        /// <param name="logToken"></param>
        /// <returns></returns>
        void InitializeLogCheckpoint(Guid logToken);

        /// <summary>
        /// Device to store index checkpoint (including overflow buckets)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IDevice GetIndexDevice(Guid token);

        /// <summary>
        /// Device to store snapshot of log (required only for snapshot checkpoints)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IDevice GetSnapshotLogDevice(Guid token);

        /// <summary>
        /// Device to store snapshot of object log (required only for snapshot checkpoints)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IDevice GetSnapshotObjectLogDevice(Guid token);


        /// <summary>
        /// Commit index checkpoint (synchronous)
        /// </summary>
        /// <param name="indexToken"></param>
        /// <param name="commitMetadata"></param>
        /// <returns></returns>
        void CommitIndexCheckpoint(Guid indexToken, byte[] commitMetadata);

        /// <summary>
        /// Commit log checkpoint (synchronous)
        /// </summary>
        /// <param name="logToken"></param>
        /// <param name="commitMetadata"></param>
        /// <returns></returns>
        void CommitLogCheckpoint(Guid logToken, byte[] commitMetadata);

        /// <summary>
        /// Retrieve commit info for previous index checkpoint
        /// </summary>
        /// <param name="indexToken"></param>
        /// <returns>Commit info, if valid checkpoint found, and null otherwise</returns>
        byte[] GetIndexCommitMetadata(Guid indexToken);

        /// <summary>
        /// Retrieve commit info for previous log checkpoint
        /// </summary>
        /// <param name="logToken"></param>
        /// <returns>Commit info, if valid checkpoint found, and null otherwise</returns>
        byte[] GetLogCommitMetadata(Guid logToken);

        /// <summary>
        /// Get latest valid checkpoint
        /// </summary>
        /// <param name="indexToken"></param>
        /// <param name="logToken"></param>
        /// <returns>true if latest valid checkpoint found, false otherwise</returns>
        bool GetLatestCheckpoint(out Guid indexToken, out Guid logToken);
    }
}